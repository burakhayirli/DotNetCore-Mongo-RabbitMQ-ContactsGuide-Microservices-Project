using AutoMapper;
using Contact.Domain;
using Contact.Domain.Dtos;
using Contact.Domain.Entities;
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
        private readonly IMapper _mapper;

        public PersonsController(IPersonRepository personRepository, IMapper mapper)
        {
            this._personRepository = personRepository;
            _mapper = mapper;
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
            if (result.Success) return Ok(_mapper.Map<ViewPersonDto>(result.Data));
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePersonDto person)
        {
            var result = await _personRepository.SaveAsync(_mapper.Map<Person>(person));
            if (result.Success) return Ok(_mapper.Map<ViewPersonDto>(result.Data));
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdatePersonDto updatePerson)
        {
            var person = await _personRepository.GetAsync(id);
            if (!person.Success) return BadRequest(person.Message);

            var updateModel = _mapper.Map<Person>(updatePerson);
            updateModel.Id = id;

            var result = await _personRepository.UpdateAsync(updateModel);

            if (result.Success) return Ok(updateModel.Id);

            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var person = await _personRepository.GetAsync(id);
            if (person.Data == null) return BadRequest(person.Message);

            var result = await _personRepository.DeleteAsync(person.Data);
            if (result.Success) return Ok();
            return BadRequest(result.Message);
        }
    }
}
