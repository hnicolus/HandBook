using SQLite;
using System;
using System.Collections.Generic;
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
        public bool IsDeleted = false;

        public bool Deleted
        {
            get
            {
                return IsDeleted;
            }
            set
            {
                IsDeleted = value;
            }
        }


    }
}
