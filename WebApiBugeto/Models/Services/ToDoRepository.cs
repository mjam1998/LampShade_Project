using System.Collections.Generic;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Entities.Context;
using System.Linq;
using System;

namespace WebApplication1.Models.Services
{
    public class ToDoRepository
    {
        private readonly DataBaseContext _context;

        public ToDoRepository(DataBaseContext context)
        {
            _context = context;
        }
        
        public List<ToDoDto> GetAll()
        {
            return _context.ToDos.Select(x => new ToDoDto
            {
                Id = x.Id,
                InsertTime = x.InsertTime,
                IsRemoved = x.IsRemoved,
                Text = x.Text,
            }).ToList();
        }
        public ToDoDto Get(int id)
        {
            return _context.ToDos.Select(x => new ToDoDto
            {
                Id = x.Id,
                InsertTime = x.InsertTime,
                IsRemoved = x.IsRemoved,
                Text = x.Text,
            }).FirstOrDefault(x=>x.Id==id);
        }
        public int Add(AddToDoDto command)
        {
            var toDo = new ToDo(command.Text, command.CategoryId);
            _context.ToDos.Add(toDo);
            _context.SaveChanges();
            return toDo.Id;
        }
        public void Delete(int id)
        {
            var toDo=_context.ToDos.FirstOrDefault(x => x.Id == id);
            toDo.Remove();
            _context.SaveChanges();
        }
        public void Edit(EditToDoDto command)
        {
            var toDo = _context.ToDos.FirstOrDefault(x => x.Id == command.Id);
            toDo.Edit(command.Text,command.CategoryId);
            _context.SaveChanges();
        }
    }
}
