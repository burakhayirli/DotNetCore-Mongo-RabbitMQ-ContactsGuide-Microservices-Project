using AutoMapper;
using Contact.Api.ValidationRules.FluentValidation;
using Contact.Domain;
using Contact.Domain.Dtos;
using Contact.Domain.Entities;
using Contact.Repository;
using ContactGuide.Shared.Aspects.Autofac.Validation;
using FluentValidation;
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
        //[ValidationAspect(typeof(CreatePersonDtoValidator), Priority = 1)]
        public async Task<IActionResult> Post([FromBody] CreatePersonDto person)
        {
            CreatePersonDtoValidator val = new CreatePersonDtoValidator();
            var valResult = val.Validate(person);
            if (!valResult.IsValid)
            {
                //throw new ValidationException(valResult.Errors);
                return BadRequest(valResult.Errors);
            }
            var result = await _personRepository.SaveAsync(_mapper.Map<Person>(person));
            if (result.Success) return Ok(_mapper.Map<ViewPersonDto>(result.Data));
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UpdatePersonDto updatePerson)
        {
            UpdatePersonDtoValidator val = new UpdatePersonDtoValidator();
            UpdatePersonDto updatedPerson = updatePerson;
            updatedPerson.Id = id;

            var valResult = val.Validate(updatedPerson);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Errors);
            }

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
