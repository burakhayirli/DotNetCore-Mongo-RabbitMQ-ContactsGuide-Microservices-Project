using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Api.HttpRequests;
using Report.Api.MQServices;
using Report.Domain;
using Report.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Report.Api.ServiceAdapters.ContactService;

namespace Report.Api.BackgroundServices
{
    public class ExcelReportCreatingProcessBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IReportRepository _reportRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IContactService _contactService;
        private IModel _channel;
        public ExcelReportCreatingProcessBackgroundService(RabbitMQClientService rabbitMQClientService,IReportRepository reportRepository, IWebHostEnvironment hostEnvironment,IContactService contactService)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _reportRepository = reportRepository;
            _hostEnvironment = hostEnvironment;
            _contactService = contactService;
            Console.WriteLine("BackgroundService is running...");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.CreateDocumentQueueName, true, consumer);
            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }
        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var createDocumentModel = JsonSerializer.Deserialize<Domain.Report>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            Console.WriteLine("Rapor oluşturma talebi karşılandı. Veriler excel formatında rapora dönüştürülüyor...");
            Console.WriteLine($"İstek Bilgisi: {createDocumentModel.Id} {createDocumentModel.DocumentStatus}");
            Task.Delay(2000).Wait();
            
            //Get Datas From ContactApi       
            DataTable table= GenerateDataTable("Contacts");

            //Create Excel
            var wb = new XLWorkbook();
            var ds = new DataSet();

            ds.Tables.Add(table);
            wb.Worksheets.Add(ds);

            var FileName = createDocumentModel.Id+"_Report.xlsx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents", FileName);
            var returnPath = "documents/" + FileName;
            Console.WriteLine("Path: " + path);

            wb.SaveAs(path);

            createDocumentModel.DocumentStatus = DocumentStatus.Completed;
            createDocumentModel.FilePath = returnPath;
            createDocumentModel.FileName = FileName;
            createDocumentModel.CreatedDate = DateTime.Now;
                        
            //Update Database
            _reportRepository.UpdateAsync(createDocumentModel);
            Console.WriteLine($"Output: {createDocumentModel.Id} {createDocumentModel.DocumentStatus} {createDocumentModel.FileName} {createDocumentModel.FilePath}");
            Console.WriteLine("Report Created Successfully");
            return Task.CompletedTask;
        }

        private DataTable GenerateDataTable(string tableName)
        {
            try
            {
                List<Person> personList = _contactService.GetAll().Result;

                DataTable table = new DataTable { TableName = tableName };

                table.Columns.Add("Location", typeof(string));
                table.Columns.Add("TotalContactCount", typeof(int));
                table.Columns.Add("TotalPhoneNumberCount", typeof(int));

                var list = from p in personList
                           group p by new
                           {
                               p.Latitude,
                               p.Longitude
                           } into pLocations
                           select new OutputModel
                           {
                               Location = pLocations.Key.Latitude + "," + pLocations.Key.Longitude,
                               TotalContactCount = pLocations.Count(),
                               TotalPhoneNumberCount = personList
                                                .Where(
                                                        x => x.Longitude == pLocations.Key.Longitude && 
                                                        x.Latitude == pLocations.Key.Latitude && 
                                                        x.ContactInfos.Exists(s=>s.ContactType==ContactType.Phone))
                                               .Count()
                           };
                foreach (var item in list)
                {
                    //Console.WriteLine($"{item.Location} {item.TotalContactCount} {item.TotalPhoneNumberCount}");
                    table.Rows.Add(item.Location, item.TotalContactCount, item.TotalPhoneNumberCount);
                }

                return table;
            }
            catch (Exception E)
            {
                Console.WriteLine("Error! " + E.Message);
                return null;
            }
        }

        public class OutputModel
        {
            public string Location { get; set; }
            public int TotalContactCount { get; set; }
            public int TotalPhoneNumberCount { get; set; }

            public override string ToString()
            {
                return Location + " " + TotalContactCount + " " + TotalPhoneNumberCount;
            }
        }
    }
}
