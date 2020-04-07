using HandBook.Core.Functions;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HandBook.Models
{
    public class Post 
    {


        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }
        public  DateTime PubDate { get; set; } 
        public bool IsFavourite { get; set; }


        public bool IsDeleted { get;set; }


    }
}
