using AutoMapper;
using Contact.Domain;
using Contact.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, CreatePersonDto>().ReverseMap();
            CreateMap<Person, ViewPersonDto>().ReverseMap();
            CreateMap<Domain.Contact, ContactDto>().ReverseMap();
        }
    }
}
