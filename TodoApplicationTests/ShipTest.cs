using CleanTodo.Application.UseCases;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace TodoApplicationTests;

public class ShipTests
{
    private Mock<IShipRepository> _shipRepositoryMock;
    private CreateShipUseCase _createShipUseCase;

    Ship ship1 = new Ship("Bateau 1", "Capitaine", 10, 100, "tester");
    Ship ship2 = new Ship("Bateau 2", "Capitaine", 10, 100, "tester");
    Ship ship3 = new Ship("Bateau 3", "Capitaine", 10, 100, "tester");
    Ship ship4 = new Ship("Bateau 4", "Capitaine", 10, 100, "tester");
    Ship ship5 = new Ship("Bateau 5", "Capitaine", 10, 100, "tester");
    Ship ship6 = new Ship("Bateau 6", "Capitaine", 10, 100, "tester");
    Ship ship7 = new Ship("Bateau 7", "Capitaine", 10, 100, "tester");
    Ship ship8 = new Ship("Bateau 8", "Capitaine", 10, 100, "tester");

    [SetUp]
    public void Setup()
    {
        _shipRepositoryMock = new Mock<IShipRepository>();
        _createShipUseCase = new CreateShipUseCase(_shipRepositoryMock.Object, new Mock<IValidator<CreateShipDto>>().Object);

        // Arrange : le port contient déjà les 8 bateaux
        _shipRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(
            new List<Ship> { ship1, ship2, ship3, ship4, ship5, ship6, ship7, ship8 });
    }

    [Test]
    public void CreateShip_WhenPortIsFull_ErrorMessage()
    {
        // Arrange
        CreateShipDto createShipDto = new CreateShipDto { Name = "Bateau 9", Captain = "Capitaine", CrewSize = 10, GoldCargo = 100 };

        // Act
        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _createShipUseCase.Execute(createShipDto, "tester"));

        // Assert
        Assert.That(ex.Message == "Le port est plein.");
    }
}