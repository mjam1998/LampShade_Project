using System;

namespace WebApplication1.Models.Services
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsRemoved { get; set; }
        
    }
}
