using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases
{
    public class CreateTodoUseCase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IValidator<CreateTodoDto> _validator;

        public CreateTodoUseCase(ITodoRepository todoRepository, IValidator<CreateTodoDto> validator)
        {
            _todoRepository = todoRepository;
            _validator = validator;
        }

    }
    
}
