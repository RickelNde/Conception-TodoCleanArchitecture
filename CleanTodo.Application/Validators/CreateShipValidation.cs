using CleanTodo.Domain.DTOS;
using FluentValidation;

namespace CleanTodo.Application.Validators;

public class CreateShipValidation : AbstractValidator<CreateShipDto>
{
    public CreateShipValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.Captain)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(x => x.CrewSize)
            .InclusiveBetween(1, 500);

        RuleFor(x => x.GoldCargo)
            .InclusiveBetween(0, 1_000_000);
    }
}