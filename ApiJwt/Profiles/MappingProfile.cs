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
            CreateMap<Venta,VentaDto>().ReverseMap();
            CreateMap<Paciente,PacienteDto>().ReverseMap();
            CreateMap<MedicamentoVendido, MedicamentoVendidoDto>().ReverseMap();
            CreateMap<MedicamentoComprado,MedicamentoCompradoDto>().ReverseMap();
            CreateMap<Medicamento, MedicamentoDto>().ReverseMap();
            CreateMap<Proveedor,ProveedorDto>().ReverseMap();
            CreateMap<Compra, CompraDto>().ReverseMap();
            CreateMap<Empleado,EmpleadoDto>().ReverseMap();
            CreateMap<Medicamento,MedicamentoXProveedor>().ReverseMap();
        }
    }
}