using FluentValidation;
using CleanTodo.Domain.DTOS;

namespace CleanTodo.Application.Validators
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Le mot de passe est requis.")
                .MinimumLength(15)
                    .WithMessage("Le mot de passe doit contenir au moins 15 caractères. " 
                                 )
                .MaximumLength(200)
                    .WithMessage("Le mot de passe ne peut pas dépasser 200 caractères.");
        }
    }
}
