
using CleanTodo.Application.UseCases;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;

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

        private GetPortsUseCase _getShipUseCase = getPortsUseCase;

        private SendShipUseCase _sendShipUseCase = sendShipUseCase;

        private IConfiguration _config = config;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipDto>>> GetAll()
        {
            var ships = await _getAllshipCase.Execute();
            return Ok(ships);
        }

        [HttpPost("CreateShip")]
        public async Task<ActionResult<ShipDto>> CreateShip([FromBody] CreateShipDto createShipDto,string createdBy)
        {
            ShipDto ship = await _createShipCase.Execute(createShipDto, createdBy);

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
        [HttpGet("ports")]
        public async Task<ActionResult<List<string>>> GetPorts()
        {
            try
            {
                var ports = await _getShipUseCase.ExecuteAsync();
                return Ok(ports);
            }
            catch (HttpRequestException ex)
            {
                // l'API du prof a refusé (401, 404...) ou est injoignable
                return StatusCode(502, new { error = ex.Message });
            }
           
        }

       
        [HttpPost("sail/{destination}")]
        public async Task<IActionResult> Sail(string destination)
        {
            try
            {
                await _sendShipUseCase.ExecuteAsync(destination);
                return Ok(new { message = $"Bateau envoyé vers {destination}" });

            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
