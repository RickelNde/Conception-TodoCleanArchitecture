using CleanTodo.Application.UseCase;
using CleanTodo.Application.UseCases;
using CleanTodo.Application.UseCases;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodo.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShipController(GetAllShipsUseCase getAllShipsUseCase,CreateShipUseCase createShipUseCase,UpdateShipUseCase updateShipUseCase, GetShipUseCase getShipUseCase,DeleteShipUseCase deleteShipUseCase) : ControllerBase
    {
        private GetAllShipsUseCase _getAllshipCase = getAllShipsUseCase;
     
        private CreateShipUseCase _createShipCase = createShipUseCase;

        private GetShipUseCase _getShipCase = getShipUseCase;

        private DeleteShipUseCase _deleteShipUseCase = deleteShipUseCase;
        private UpdateShipUseCase _updateShipUseCase = updateShipUseCase;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipDto>>> GetAll()
        {
            var ships = await _getAllshipCase.Execute();
            return Ok(ships);
        }

        [HttpPost("CreateShip")]
        public async Task<ActionResult<ShipDto>> CreateShip([FromBody] CreateShipDto createShipDto,string createdBy)
        {
            ShipDto ship = await createShipUseCase.Execute(createShipDto, createdBy);

            return CreatedAtAction(
                nameof(GetId),
                new { id = ship.Id },
                ship);

        }


        [HttpGet("GetShipId")]
        public async Task<IActionResult> GetId(Guid id)
        {
            try
            {
                ShipDto ship = await _getShipCase.Execute(id);
                return Ok(ship);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("DeleteShip")]
        public async Task<IActionResult> DeleteShip(Guid id)
        {
            try
            {
                await _deleteShipUseCase.Execute(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShipDto>> Update(Guid id, [FromBody] UpdateShipDto updateShipDto)
        {
            try
            {
                ShipDto ship = await _updateShipUseCase.Execute(id, updateShipDto);
                return Ok(ship);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
