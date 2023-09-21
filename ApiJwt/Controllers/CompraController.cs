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
    public class CompraController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompraController (IUnitOfWork unitOfWork, IMapper mapper){
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Compra>> Post(CompraDto compraDto){
            var Compra = _mapper.Map<Compra>(compraDto);
            if (Compra == null){
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Compras.Add(Compra);
            await _unitOfWork.SaveAsync();
            compraDto.Id = Compra.Id;
            return CreatedAtAction(nameof(Post),new {id = compraDto.Id},  compraDto);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<CompraDto>>> Get ([FromQuery] Params CompraParams) {
            var Compras = await _unitOfWork.Compras.GetAllAsync(CompraParams.PageIndex,CompraParams.PageSize,CompraParams.Search);
            var ComprasListDto = _mapper.Map<List<CompraDto>>(Compras.registers);
            return new Pager<CompraDto>(ComprasListDto,Compras.totalRegisters,CompraParams.PageIndex,CompraParams.PageSize,CompraParams.Search);
        }
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CompraDto>> Put (int id, [FromBody]CompraDto CompraDto){
            if(CompraDto == null){return NotFound(new ApiResponse(404));}
            var compra = _mapper.Map<Compra>(CompraDto);
            _unitOfWork.Compras.Update(compra);
            await _unitOfWork.SaveAsync();
            return CompraDto;
        }
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete (int id){
            var Compra = await _unitOfWork.Compras.GetByIdAsync(id);
            if (Compra == null){return BadRequest(new ApiResponse(400));}
            _unitOfWork.Compras.Remove(Compra);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }        
    }
}