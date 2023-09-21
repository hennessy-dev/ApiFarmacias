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
            CreateMap<Medicamento, MedicamentoDto>().ReverseMap();
            CreateMap<Proveedor,ProveedorDto>().ReverseMap();
            CreateMap<Compra, CompraDto>().ReverseMap();
        }
    }
}