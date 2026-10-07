using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases.Todo
{
    public class ToggleTodoUseCase
    {
        private readonly ITodoRepository _todoRepository;

        public ToggleTodoUseCase(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task Execute(Guid id)
        {
            var todo = await _todoRepository.FindById(id);

            if (todo is null)
            {
                throw new NotFoundException("le todo n'existe pas");
            }

            todo.IsCompleted = !todo.IsCompleted;

            await _todoRepository.UpdateTodo(todo);
        }

    }
}
