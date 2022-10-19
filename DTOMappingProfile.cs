using AutoMapper;
using dchv_api.DTOs;
using dchv_api.Models;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<Contact, ContactDTO>();
        CreateMap<Role, RoleDTO>();
        CreateMap<Login, LoginDTO>();
        CreateMap<Person, PersonDTO>();
        CreateMap<Record, RecordDTO>();
        CreateMap<RecordData, RecordDataDTO>();
    }
}