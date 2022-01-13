using Contact.Domain;
using Contact.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _personRepository.GetAllAsync();
            if (result.Success) return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _personRepository.GetAsync(id);
            if (result.Success) return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            var result = await _personRepository.SaveAsync(person);
            if (result.Success) return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Person updatePerson)
        {
            var person = await _personRepository.GetAsync(id);
            if (!person.Success) return BadRequest(person.Message);

            updatePerson.Id = id;

            var result = await _personRepository.UpdateAsync(updatePerson);

            if (result.Success)
            {
                return Ok(updatePerson.Id);
            }

            return BadRequest("Cannot update");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var person = await _personRepository.GetAsync(id);
            if (person.Data == null) return BadRequest("Person info does not exist");

            var result = await _personRepository.DeleteAsync(person.Data);
            if (result.Success) return Ok();
            return BadRequest(result.Message);
        }
    }
}
