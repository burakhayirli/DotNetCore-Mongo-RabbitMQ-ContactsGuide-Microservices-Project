using AutoMapper;
using Contact.Domain.Dtos;
using Contact.Domain.Entities;
using Contact.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Api.Controllers
{
    [Route("api/persons/{personId}/[controller]")]
    [ApiController]
    public class ContactInfosController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public ContactInfosController(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string personId)
        {
            var person = await _personRepository.GetAsync(personId);
            if (!person.Success) return BadRequest(person.Message);

            if (person.Data.ContactInfos.Count < 0) return BadRequest("Contact Info not found belongs to person");

            return Ok(person.Data.ContactInfos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string personId, ContactInfoDto contactInfoDto)
        {
            var person = await _personRepository.GetAsync(personId);
            if (!person.Success) return BadRequest(person.Message);

            person.Data.ContactInfos.Add(_mapper.Map<ContactInfo>(contactInfoDto));
            var result = await _personRepository.UpdateAsync(person.Data);
            if (!result.Success) return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("{contactInfoId}")]
        public async Task<IActionResult> Update(string personId, string contactInfoId, ContactInfoDto contactInfoDto)
        {
            var person = await _personRepository.GetAsync(personId);
            if (person.Data == null) return BadRequest(person.Message);

            var index = person.Data.ContactInfos.FindIndex(x => x.Id.Equals(contactInfoId));
            if (index == -1)
                return BadRequest("Contact Info not found");

            person.Data.ContactInfos[index] = _mapper.Map<ContactInfo>(contactInfoDto);

            var result = await _personRepository.UpdateAsync(person.Data);
            if (!result.Success) return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("{contactInfoId}")]
        public async Task<IActionResult> Delete(string personId, string contactInfoId)
        {
            var person = await _personRepository.GetAsync(personId);
            if (person.Data == null) return BadRequest(person.Message);

            var index = person.Data.ContactInfos.FindIndex(x => x.Id.Equals(contactInfoId));
            if (index == -1)
                return BadRequest("Contact Info not found");

            person.Data.ContactInfos.RemoveAt(index);
            var result = await _personRepository.UpdateAsync(person.Data);
            if (!result.Success) return BadRequest(result.Message);
            return Ok();
        }
    }
}
