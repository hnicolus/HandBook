using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Models
{
    public class Note 
    {

       [PrimaryKey,AutoIncrement]
       public int Id { get; set; }

       [MaxLength(30)]
       public string Title { get; set; }

        public DateTime Date_pub { get; set; }
       public string Body { get; set; } 

       public bool Islocked { get; set; }
    }
}
