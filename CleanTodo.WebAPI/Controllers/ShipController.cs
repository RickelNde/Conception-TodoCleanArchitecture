
using CleanTodo.Application.UseCases;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodo.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShipController(GetAllShipsUseCase getAllShipsUseCase,GetPortsUseCase getPortsUseCase,SendShipUseCase sendShipUseCase , IConfiguration config,DockShipUseCase dockShipUseCase, CreateShipUseCase createShipUseCase,UpdateShipUseCase updateShipUseCase, GetShipUseCase getShipUseCase,DeleteShipUseCase deleteShipUseCase) : ControllerBase
    {
        private GetAllShipsUseCase _getAllshipCase = getAllShipsUseCase;
     
        private CreateShipUseCase _createShipCase = createShipUseCase;
        private DockShipUseCase _dockShipUseCase = dockShipUseCase;

        private GetShipUseCase _getShipCase = getShipUseCase;

        private DeleteShipUseCase _deleteShipUseCase = deleteShipUseCase;
        private UpdateShipUseCase _updateShipUseCase = updateShipUseCase;

        private GetPortsUseCase _getPortsUseCase = getPortsUseCase;

        private SendShipUseCase _sendShipUseCase = sendShipUseCase;

        [Authorize]
        [HttpGet("GetAllShips")]
        public async Task<ActionResult<IEnumerable<ShipDto>>> GetAll()
        {
            var ships = await _getAllshipCase.Execute();
            return Ok(ships);
        }
        [Authorize]
        [HttpPost("CreateShip")]
        public async Task<ActionResult<ShipDto>> CreateShip([FromBody] CreateShipDto createShipDto,string createdBy)
        {
            ShipDto ship = await _createShipCase.Execute(createShipDto, createdBy);

            return CreatedAtAction(
                nameof(GetId),
                new { id = ship.Id },
                ship);

        }

        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPut("UpdateShip")]
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

        [HttpPost("dock")]
        public async Task<IActionResult> Dock([FromBody] DockShipDto dockShipDto)
        {
            try
            {
                ShipDto ship = await _dockShipUseCase.Execute(dockShipDto);
                return Ok(ship);
            }
            catch (NotFoundException )
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpGet("ports")]
        public async Task<ActionResult<List<string>>> GetPorts()
        {
            try
            {
                var ports = await _getPortsUseCase.ExecuteAsync();
                return Ok(ports);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { error = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("{id}/sail/{destination}")]
        public async Task<IActionResult> Sail(Guid id, string destination)
        {
            try
            {
                var success = await _sendShipUseCase.ExecuteAsync(id, destination);
                if (!success)
                    return BadRequest(new { message = "Impossible d'envoyer le navire (port plein ou en réparation ?)." });

                return Ok(new { message = "Le navire a été envoyé avec succès !" });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }//test la limite des bateaux 
           
        }
    }
}
