using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HandBook.Models
{
    public class ToDoTask : Post
    {
        public string EventName { get; set; }
        public string Organizer { get; set; }
        public string ContactId { get; set; }
        public int Capacity { get; set; }

        public bool IsAllDay { get; set; }
        public DateTime StartTime { get; set; }
        public bool Notify { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime EndTime { get; set; }
        public string Detail { get; set; }
        public bool Completed { get; set; }

  

    }
}
