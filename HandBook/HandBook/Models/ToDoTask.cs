using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Models
{
    public class ToDoTask : Post
    {
        public DateTime StartTime { get; set; }
        public bool Notify { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime EndTime { get; set; }
        public string Detail { get; set; }
        public bool Completed { get; set; }

  

    }
}
