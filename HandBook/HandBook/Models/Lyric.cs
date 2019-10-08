using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Models
{
    public class Lyric
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }

        [MaxLength(30)]
        public string Genre { get; set; }
        public string Chorus {get;set;}
        public string Verse { get; set; }


    }
}
