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
    public class PacienteController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PacienteController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Paciente>> Post(PacienteDto PacienteDto)
        {
            var Paciente = _mapper.Map<Paciente>(PacienteDto);
            if (Paciente == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Pacientes.Add(Paciente);
            await _unitOfWork.SaveAsync();
            PacienteDto.Id = Paciente.Id;
            return CreatedAtAction(nameof(Post), new { id = PacienteDto.Id }, PacienteDto);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PacienteDto>>> Get([FromQuery] Params PacienteDto)
        {
            var Pacientes = await _unitOfWork.Pacientes.GetAllAsync(
                PacienteDto.PageIndex,
                PacienteDto.PageSize,
                PacienteDto.Search
            );
            var PacientesListDto = _mapper.Map<List<PacienteDto>>(Pacientes.registers);
            return new Pager<PacienteDto>(
                PacientesListDto,
                Pacientes.totalRegisters,
                PacienteDto.PageIndex,
                PacienteDto.PageSize,
                PacienteDto.Search
            );
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PacienteDto>> Put(int id, [FromBody] PacienteDto PacienteDto)
        {
            if (PacienteDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Paciente = _mapper.Map<Paciente>(PacienteDto);
            _unitOfWork.Pacientes.Update(Paciente);
            await _unitOfWork.SaveAsync();
            return PacienteDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);
            if (Paciente == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Pacientes.Remove(Paciente);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet("PatientsWhoHavePurchased")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> PatientsWhoHavePurchased(
            string drugName
        )
        {
            try
            {
                var pacientes = await _unitOfWork.Pacientes.PatientsWhoHavePurchased(drugName);
                return _mapper.Map<List<PacienteDto>>(pacientes);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(
                    new ApiResponse(400, "El nombre del medicamento no puede ser null")
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }

        [HttpGet("PatientWhoSpentMostMoney")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PatientWhoSpentMostMoney>> PatientWhoSpentMostMoney()
        {
            try
            {
                var (p, total) = await _unitOfWork.Pacientes.PatientWhoSpentMostMoney();
                var newP =_mapper.Map<PatientWhoSpentMostMoney>(p);
                newP.TotalGastado = total;
                return newP;
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
        [HttpGet("PatientsWhoBoughtInLastYear")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> PatientsWhoBoughtInLastYear(
            string drugName
        )
        {
            try
            {
                var pacientes = await _unitOfWork.Pacientes.PatientsWhoBoughtInLastYear(drugName);
                return _mapper.Map<List<PacienteDto>>(pacientes);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(
                    new ApiResponse(400, "El nombre del medicamento no puede ser null")
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
        [HttpGet("PatientsWhoHaventBoughtAnythingBetween")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> PatientsWhoHaventBoughtAnythingBetween(
            DateTime firtsDate,
            DateTime lastDate
        )
        {
            try
            {
                var pacientes = await _unitOfWork.Pacientes.PatientsWhoHaventBoughtAnythingBetween(firtsDate,lastDate);
                return _mapper.Map<List<PacienteDto>>(pacientes);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(
                    new ApiResponse(400, "Las fechas no pueden ser nulas")
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
        [HttpGet("PatientsTotalSpentInLastYear")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<PatientWhoSpentMostMoney>>> PatientsTotalSpentInLastYear()
        {
            try
            {
                var (pacientes,totalGastado) = await _unitOfWork.Pacientes.PatientsTotalSpentInLastYear();
                List<PatientWhoSpentMostMoney> pacientesDto = new();
               for (int i = 0; i < pacientes.Count; i++)
               {
                    var paciente = _mapper.Map<PatientWhoSpentMostMoney>(pacientes[i]);
                    paciente.TotalGastado = totalGastado[i];
                    pacientesDto.Add(paciente);
               }
               return pacientesDto;
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, "Se produjo un error: " + ex.Message));
            }
        }
        
    }
}
