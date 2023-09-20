using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJwt.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiJwt.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile (){
            CreateMap<Medicamento, MedicamentoDto>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<Medicamento,MedicamentoXProveedorDto>().ReverseMap();
            CreateMap<Proveedor,ProveedorDto>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}