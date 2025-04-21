using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entities
{
    public class ToDo//یادداشتها
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime  InsertTime { get; set; }
        public bool IsRemoved { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }//برای ارتباط چند به چند بصورت اتوماتیک

        public ToDo( string text,  int categoryId)
        {
           
            Text = text;
            InsertTime = DateTime.Now;
            IsRemoved = false;
            CategoryId = categoryId;
        }

        public void Edit(string text, int categoryId)
        {
            Text = text;
            CategoryId = categoryId;
        }
        public void Remove()
        {
            IsRemoved = true;
        }
    }
}
