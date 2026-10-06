using CleanTodo.Application.UseCase;
using CleanTodo.Application.UseCases;
using CleanTodo.Application.UseCases.User;
using CleanTodo.Infrastructure.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanTodo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // cette ligne ajoute les validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
       services.AddScoped<CreateUserUseCase>();
      // services.AddScoped<DeleteTodoUseCase>();
        services.AddScoped<GetTodoUseCase>();
        services.AddScoped<GetAllUsersUseCase>();
        // services.AddScoped<ToggleTodoCompleteStatusUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<RegisterUseCase>();
        services.AddScoped<JwtService>();
        services.AddScoped<DeleteTodoUseCase>();

        return services;
    }
}