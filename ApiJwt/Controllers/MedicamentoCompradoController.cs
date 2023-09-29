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
    public class MedicamentoCompradoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MedicamentoCompradoController (IUnitOfWork unitOfWork, IMapper mapper){
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicamentoComprado>> Post(MedicamentoCompradoDto MedicamentoCompradoDto){
            var MedicamentoComprado = _mapper.Map<MedicamentoComprado>(MedicamentoCompradoDto);
            if (MedicamentoComprado == null){
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.MedicamentosComprados.Add(MedicamentoComprado);
            await _unitOfWork.SaveAsync();
            MedicamentoCompradoDto.Id = MedicamentoComprado.Id;
            return CreatedAtAction(nameof(Post),new {id = MedicamentoCompradoDto.Id},  MedicamentoCompradoDto);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<MedicamentoCompradoDto>>> Get ([FromQuery] Params MedicamentoCompradoDto) {
            var MedicamentosComprados = await _unitOfWork.MedicamentosComprados.GetAllAsync(MedicamentoCompradoDto.PageIndex,MedicamentoCompradoDto.PageSize,MedicamentoCompradoDto.Search);
            var MedicamentosCompradosListDto = _mapper.Map<List<MedicamentoCompradoDto>>(MedicamentosComprados.registers);
            return new Pager<MedicamentoCompradoDto>(MedicamentosCompradosListDto,MedicamentosComprados.totalRegisters,MedicamentoCompradoDto.PageIndex,MedicamentoCompradoDto.PageSize,MedicamentoCompradoDto.Search);
        }
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicamentoCompradoDto>> Put (int id, [FromBody]MedicamentoCompradoDto MedicamentoCompradoDto){
            if(MedicamentoCompradoDto == null){return NotFound(new ApiResponse(404));}
            var MedicamentoComprado = _mapper.Map<MedicamentoComprado>(MedicamentoCompradoDto);
            _unitOfWork.MedicamentosComprados.Update(MedicamentoComprado);
            await _unitOfWork.SaveAsync();
            return MedicamentoCompradoDto;
        }
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete (int id){
            var MedicamentoComprado = await _unitOfWork.MedicamentosComprados.GetByIdAsync(id);
            if (MedicamentoComprado == null){return BadRequest(new ApiResponse(400));}
            _unitOfWork.MedicamentosComprados.Remove(MedicamentoComprado);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }        
        [HttpGet("GetDrugPurchasedFrom")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ICollection<MedicamentoCompradoDto>>> GetDrugPurchasedFrom (int ProveedorId){
            var Medicamentos = await _unitOfWork.MedicamentosComprados.GetDrugPurchasedFrom(ProveedorId);
            return _mapper.Map<List<MedicamentoCompradoDto>>(Medicamentos);
        }
    }
}