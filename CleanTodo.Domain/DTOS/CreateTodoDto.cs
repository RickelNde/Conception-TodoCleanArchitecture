using CleanTodo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Domain.DTOS
{
    public class CreateTodoDto
    {
        public string Title { get; set; }

        public CreateTodoDto() { }

        public CreateTodoDto(Todo todo)
        {
            Title = todo.Text;
        }
    }
}
