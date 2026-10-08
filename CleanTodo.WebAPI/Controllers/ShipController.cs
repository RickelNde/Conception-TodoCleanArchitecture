using CleanTodo.Application.UseCase;
using CleanTodo.Application.UseCases;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodo.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShipController(GetAllShipsUseCase getAllShipsUseCase,CreateShipUseCase createShipUseCase,GetShipUseCase getShipUseCase) : ControllerBase
    {
        private GetAllShipsUseCase _getAllshipCase = getAllShipsUseCase;
     
        private CreateShipUseCase _createShipCase = createShipUseCase;

        private GetShipUseCase _getShipCase = getShipUseCase;

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
                nameof(Get),
                new { id = ship.Id },
                ship);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
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
    }
}
