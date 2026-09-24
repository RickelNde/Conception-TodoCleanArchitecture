using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

namespace CleanTodo.Application.UseCase;

public class RegisterUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterDTO> _validator;

    public RegisterUseCase(IUserRepository userRepository, IValidator<RegisterDTO> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<User> Execute(RegisterDTO registerDto)
    {
        var validationResult = await _validator.ValidateAsync(registerDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        User user = new User(registerDto.Username, hashedPassword);

        return await _userRepository.AddUser(user);
    }
}