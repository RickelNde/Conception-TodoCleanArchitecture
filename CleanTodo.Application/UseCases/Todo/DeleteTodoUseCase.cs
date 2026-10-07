using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases
{
    public class DeleteTodoUseCase
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoUseCase(ITodoRepository todoRepository)
        {
            _todoRepository= todoRepository;
        }
        public async Task Execute(Guid id)
        {
            var todo= await _todoRepository.FindById(id);
            if (todo is null)
            {
                throw new NotFoundException("le todo n'existe pas");
            }

            await _todoRepository.DeleteTodo(id);
        }
    }
        
     
}
