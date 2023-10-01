using System.Text.RegularExpressions;
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
    public class MedicamentoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicamentoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Medicamento>> Post(MedicamentoDto MedicamentoDto)
        {
            var Medicamento = _mapper.Map<Medicamento>(MedicamentoDto);
            if (Medicamento == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Medicamentos.Add(Medicamento);
            await _unitOfWork.SaveAsync();
            MedicamentoDto.Id = Medicamento.Id;
            return CreatedAtAction(nameof(Post), new { id = MedicamentoDto.Id }, MedicamentoDto);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<MedicamentoDto>>> Get(
            [FromQuery] Params MedicamentoDto
        )
        {
            var Medicamentos = await _unitOfWork.Medicamentos.GetAllAsync(
                MedicamentoDto.PageIndex,
                MedicamentoDto.PageSize,
                MedicamentoDto.Search
            );
            var MedicamentosListDto = _mapper.Map<List<MedicamentoDto>>(Medicamentos.registers);
            return new Pager<MedicamentoDto>(
                MedicamentosListDto,
                Medicamentos.totalRegisters,
                MedicamentoDto.PageIndex,
                MedicamentoDto.PageSize,
                MedicamentoDto.Search
            );
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicamentoDto>> Put(
            int id,
            [FromBody] MedicamentoDto MedicamentoDto
        )
        {
            if (MedicamentoDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Medicamento = _mapper.Map<Medicamento>(MedicamentoDto);
            _unitOfWork.Medicamentos.Update(Medicamento);
            await _unitOfWork.SaveAsync();
            return MedicamentoDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
            if (Medicamento == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Medicamentos.Remove(Medicamento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet("GetLessThan50")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetLessThan50()
        {
            var ListMedicamentos = await _unitOfWork.Medicamentos.GetLessThan50();
            return _mapper.Map<List<MedicamentoDto>>(ListMedicamentos);
        }

        [HttpGet("GetMedicamentoProveedor")]
        [MapToApiVersion("1.0")]
        public async Task<
            ActionResult<IEnumerable<MedicamentoXProveedor>>
        > GetMedicamentoProveedor()
        {
            var ListMedicamentos = await _unitOfWork.Medicamentos.GetMedicamentoProveedor();
            return _mapper.Map<List<MedicamentoXProveedor>>(ListMedicamentos);
        }

        [HttpGet("GetDrugExpiresBefore")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetDrugExpiresBefore(
            DateTime dateTime
        )
        {
            try
            {
                Console.WriteLine(dateTime);
                var ListMedicamentos = await _unitOfWork.Medicamentos.GetDrugExpiresBefore(
                    dateTime
                );
                return _mapper.Map<List<MedicamentoDto>>(ListMedicamentos);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new ApiResponse(400, "La fecha es nula o está mal formateada."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
        [HttpGet("GetUnsoldDrug")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetUnsoldDrug()
        {
            try
            {
                var ListMedicamentos = await _unitOfWork.Medicamentos.GetUnsoldDrug();
                return _mapper.Map<List<MedicamentoDto>>(ListMedicamentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
        [HttpGet("GetMostExpensiveDrug")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicamentoDto>> GetMostExpensiveDrug()
        {
            try
            {
                var medicamento = await _unitOfWork.Medicamentos.GetMostExpensiveDrug();
                return _mapper.Map<MedicamentoDto>(medicamento);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
        [HttpGet("GetLeastSoldDrug")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicamentoDto>> GetLeastSoldDrug()
        {
            try
            {
                var medicamento = await _unitOfWork.Medicamentos.GetLeastSoldDrug();
                return _mapper.Map<MedicamentoDto>(medicamento);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
    }
}
