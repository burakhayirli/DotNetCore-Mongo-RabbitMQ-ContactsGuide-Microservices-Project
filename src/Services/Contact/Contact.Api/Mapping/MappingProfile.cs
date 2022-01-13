using AutoMapper;
using Contact.Domain.Dtos;
using Contact.Domain.Entities;

namespace Contact.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, CreatePersonDto>().ReverseMap();
            CreateMap<Person, UpdatePersonDto>().ReverseMap();
            CreateMap<Person, ViewPersonDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
        }
    }
}
