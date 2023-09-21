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
    public class EmpleadoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmpleadoController (IUnitOfWork unitOfWork, IMapper mapper){
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Empleado>> Post(EmpleadoDto EmpleadoDto){
            var Empleado = _mapper.Map<Empleado>(EmpleadoDto);
            if (Empleado == null){
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Empleados.Add(Empleado);
            await _unitOfWork.SaveAsync();
            EmpleadoDto.Id = Empleado.Id;
            return CreatedAtAction(nameof(Post),new {id = EmpleadoDto.Id},  EmpleadoDto);

        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<EmpleadoDto>>> Get ([FromQuery] Params EmpleadoDto) {
            var Empleados = await _unitOfWork.Empleados.GetAllAsync(EmpleadoDto.PageIndex,EmpleadoDto.PageSize,EmpleadoDto.Search);
            var EmpleadosListDto = _mapper.Map<List<EmpleadoDto>>(Empleados.registers);
            return new Pager<EmpleadoDto>(EmpleadosListDto,Empleados.totalRegisters,EmpleadoDto.PageIndex,EmpleadoDto.PageSize,EmpleadoDto.Search);
        }
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<EmpleadoDto>> Put (int id, [FromBody]EmpleadoDto EmpleadoDto){
            if(EmpleadoDto == null){return NotFound(new ApiResponse(404));}
            var Empleado = _mapper.Map<Empleado>(EmpleadoDto);
            _unitOfWork.Empleados.Update(Empleado);
            await _unitOfWork.SaveAsync();
            return EmpleadoDto;
        }
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete (int id){
            var Empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
            if (Empleado == null){return BadRequest(new ApiResponse(400));}
            _unitOfWork.Empleados.Remove(Empleado);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }        
    }
}