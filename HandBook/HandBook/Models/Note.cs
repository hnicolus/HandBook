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
       public string title { get; set; }

       public string body { get; set; } 

       public bool isFavourite { get; set; }
    }
}
