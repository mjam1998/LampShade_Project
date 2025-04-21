using System.Collections.Generic;

namespace WebApplication1.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ToDoId { get; set; }
        public ICollection<ToDo> ToDos { get; set; }

        public Category( string name, int toDoId)
        {
            
            Name = name;
            ToDoId = toDoId;
        }
        public void Edit(string name, int toDoId)
        {
            Name = name;
            ToDoId = toDoId;
        }
    }
}
