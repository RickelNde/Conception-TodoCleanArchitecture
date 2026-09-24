using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCase
{
    public class CreateUserUseCase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IValidator<CreateTodoDto> _validator;

        public CreateUserUseCase(ITodoRepository todoRepository, IValidator<CreateTodoDto> validator)
        {
            _todoRepository = todoRepository;
            _validator = validator;
        }
        public async Task<TodoDto> Execute(CreateTodoDto createTodoDto)
        {
            await _validator.ValidateAndThrowAsync(createTodoDto);

            var todo = new Todo(createTodoDto.Title);
            var createdTodo = await _todoRepository.AddTodo(todo);
            return new TodoDto(createdTodo);
        }

    }
    
}
