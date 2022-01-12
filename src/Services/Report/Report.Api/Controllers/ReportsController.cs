using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Api.HttpRequests;
using Report.Api.MQServices;
using Report.Domain;
using Report.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private RabbitMQClientService _rabbitMQClientService;
        private RabbitMQPublisher _rabbitMQPublisher;

        public ReportsController(RabbitMQClientService rabbitMQClientService, RabbitMQPublisher rabbitMQPublisher, IReportRepository reportRepository)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _rabbitMQPublisher = rabbitMQPublisher;
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reportRepository.GetAllAsync();
            if (result.Success) return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create()
        {
            var model = new Domain.Report()
            {
                DocumentStatus = DocumentStatus.Creating,
                RequestDate = DateTime.Now
            };

            var addedReport = await _reportRepository.SaveAsync(model);
            if (addedReport.Success)
            {
                _rabbitMQPublisher.Publish(model);
                return Ok(addedReport.Data);
            }
            return BadRequest("Rapor oluşturulamadı.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var doc = await _reportRepository.GetAsync(id);

            if (doc.Data == null) return BadRequest(doc.Message);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/",doc.Data.FilePath);

            if (!System.IO.File.Exists(path))
            {
                return BadRequest("Document not found");
            }

            System.IO.File.Delete(path);

            var result = await _reportRepository.DeleteAsync(doc.Data);
            if (result.Success) return Ok();
            return BadRequest(result.Message);
        }

        [HttpGet("GetDatasFromContactApi")]
        public IActionResult GetDatasFromContactApi()
        {
            HttpClient myCLient = Base.GetClientConnection();
            HttpResponseMessage response = myCLient.GetAsync("persons").Result;

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var persons = JsonConvert.DeserializeObject<ICollection<Person>>(result);
                return Ok(persons);
            }
            return BadRequest();
        }

        private void ListenQueue()
        {
            var channel = _rabbitMQClientService.Connect();
            var consumerEvent = new AsyncEventingBasicConsumer(channel);
            consumerEvent.Received += (sender, @event) =>
            {
                Task.Delay(2000);
                var model = JsonConvert.DeserializeObject<Domain.Report>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                model.DocumentStatus = DocumentStatus.Completed;
                Console.WriteLine($"Received Document: {model.DocumentStatus}");
                Console.WriteLine($"Received Document Url: {model.FilePath}");
                return Task.CompletedTask;
            };
            channel.BasicConsume(RabbitMQClientService.CreateDocumentQueueName, true, consumerEvent);
            Console.WriteLine("Queue Listening...");
        }
    }
}
