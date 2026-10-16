
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
    public class ShipController(GetAllShipsUseCase getAllShipsUseCase , IConfiguration config,DockShipUseCase dockShipUseCase, CreateShipUseCase createShipUseCase,UpdateShipUseCase updateShipUseCase, GetShipUseCase getShipUseCase,DeleteShipUseCase deleteShipUseCase) : ControllerBase
    {
        private GetAllShipsUseCase _getAllshipCase = getAllShipsUseCase;
     
        private CreateShipUseCase _createShipCase = createShipUseCase;
        private DockShipUseCase _dockShipUseCase = dockShipUseCase;

        private GetShipUseCase _getShipCase = getShipUseCase;

        private DeleteShipUseCase _deleteShipUseCase = deleteShipUseCase;
        private UpdateShipUseCase _updateShipUseCase = updateShipUseCase;

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
            string token = Request.Headers["Authorization"].ToString();

            if (token != "Bearer " + _config["Broker:AppToken"])
            {
                return Unauthorized();
            }

            try
            {
                ShipDto ship = await _dockShipUseCase.Execute(dockShipDto);
                return Ok(ship);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }
}
