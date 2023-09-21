using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using ApiJwt.Dtos;
using ApiJwt.Helpers;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;

namespace ApiJwt.Controllers
{
    [Authorize(Roles = "Employee,Manager,Admin")]
    public class ProveedorController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProveedorController (IUnitOfWork unitOfWork, IMapper mapper){
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Proveedor>> Post(ProveedorDto proveedorDto){
            var Proveedor = _mapper.Map<Proveedor>(proveedorDto);
            if (Proveedor == null){
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Proveedores.Add(Proveedor);
            await _unitOfWork.SaveAsync();
            proveedorDto.Id = Proveedor.Id;
            return CreatedAtAction(nameof(Post),new {id = proveedorDto.Id},  proveedorDto);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<ProveedorDto>>> Get ([FromQuery] Params ProveedorParams) {
            var Proveedores = await _unitOfWork.Proveedores.GetAllAsync(ProveedorParams.PageIndex,ProveedorParams.PageSize,ProveedorParams.Search);
            var ProveedoresListDto = _mapper.Map<List<ProveedorDto>>(Proveedores.registers);
            return new Pager<ProveedorDto>(ProveedoresListDto,Proveedores.totalRegisters,ProveedorParams.PageIndex,ProveedorParams.PageSize,ProveedorParams.Search);
        }
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ProveedorDto>> Put (int id, [FromBody]ProveedorDto ProveedorDto){
            if(ProveedorDto == null){return NotFound(new ApiResponse(404));}
            var Proveedor = _mapper.Map<Proveedor>(ProveedorDto);
            _unitOfWork.Proveedores.Update(Proveedor);
            await _unitOfWork.SaveAsync();
            return ProveedorDto;
        }
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete (int id){
            var Proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
            if (Proveedor == null){return BadRequest(new ApiResponse(400));}
            _unitOfWork.Proveedores.Remove(Proveedor);
            await _unitOfWork.SaveAsync();
            return NoContent();

        }

        
    }
}