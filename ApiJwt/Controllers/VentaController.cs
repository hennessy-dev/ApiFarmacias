using API.Helpers;
using ApiJwt.Dtos;
using ApiJwt.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiJwt.Controllers
{
    [Authorize(Roles = "Employee,Manager,Admin")]
    public class VentaController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VentaController (IUnitOfWork unitOfWork, IMapper mapper){
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Venta>> Post(VentaDto VentaDto){
            var Venta = _mapper.Map<Venta>(VentaDto);
            if (Venta == null){
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Ventas.Add(Venta);
            await _unitOfWork.SaveAsync();
            VentaDto.Id = Venta.Id;
            return CreatedAtAction(nameof(Post),new {id = VentaDto.Id},  VentaDto);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<VentaDto>>> Get ([FromQuery] Params VentaDto) {
            var Ventas = await _unitOfWork.Ventas.GetAllAsync(VentaDto.PageIndex,VentaDto.PageSize,VentaDto.Search);
            var VentasListDto = _mapper.Map<List<VentaDto>>(Ventas.registers);
            return new Pager<VentaDto>(VentasListDto,Ventas.totalRegisters,VentaDto.PageIndex,VentaDto.PageSize,VentaDto.Search);
        }
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<VentaDto>> Put (int id, [FromBody]VentaDto VentaDto){
            if(VentaDto == null){return NotFound(new ApiResponse(404));}
            var Venta = _mapper.Map<Venta>(VentaDto);
            _unitOfWork.Ventas.Update(Venta);
            await _unitOfWork.SaveAsync();
            return VentaDto;
        }
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete (int id){
            var Venta = await _unitOfWork.Ventas.GetByIdAsync(id);
            if (Venta == null){return BadRequest(new ApiResponse(400));}
            _unitOfWork.Ventas.Remove(Venta);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }        
    }
}