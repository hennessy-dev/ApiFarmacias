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
    public class MedicamentoVendidoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MedicamentoVendidoController (IUnitOfWork unitOfWork, IMapper mapper){
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicamentoVendido>> Post(MedicamentoVendidoDto MedicamentoVendidoDto){
            var MedicamentoVendido = _mapper.Map<MedicamentoVendido>(MedicamentoVendidoDto);
            if (MedicamentoVendido == null){
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.MedicamentosVendidos.Add(MedicamentoVendido);
            await _unitOfWork.SaveAsync();
            MedicamentoVendidoDto.Id = MedicamentoVendido.Id;
            return CreatedAtAction(nameof(Post),new {id = MedicamentoVendidoDto.Id},  MedicamentoVendidoDto);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<MedicamentoVendidoDto>>> Get ([FromQuery] Params MedicamentoVendidoDto) {
            var MedicamentosVendidos = await _unitOfWork.MedicamentosVendidos.GetAllAsync(MedicamentoVendidoDto.PageIndex,MedicamentoVendidoDto.PageSize,MedicamentoVendidoDto.Search);
            var MedicamentosVendidosListDto = _mapper.Map<List<MedicamentoVendidoDto>>(MedicamentosVendidos.registers);
            return new Pager<MedicamentoVendidoDto>(MedicamentosVendidosListDto,MedicamentosVendidos.totalRegisters,MedicamentoVendidoDto.PageIndex,MedicamentoVendidoDto.PageSize,MedicamentoVendidoDto.Search);
        }
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicamentoVendidoDto>> Put (int id, [FromBody]MedicamentoVendidoDto MedicamentoVendidoDto){
            if(MedicamentoVendidoDto == null){return NotFound(new ApiResponse(404));}
            var MedicamentoVendido = _mapper.Map<MedicamentoVendido>(MedicamentoVendidoDto);
            _unitOfWork.MedicamentosVendidos.Update(MedicamentoVendido);
            await _unitOfWork.SaveAsync();
            return MedicamentoVendidoDto;
        }
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete (int id){
            var MedicamentoVendido = await _unitOfWork.MedicamentosVendidos.GetByIdAsync(id);
            if (MedicamentoVendido == null){return BadRequest(new ApiResponse(400));}
            _unitOfWork.MedicamentosVendidos.Remove(MedicamentoVendido);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }        
    }
}