using HandBook.Core.Functions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Models
{
    public class Lyric : Post
    {

        [MaxLength(30)]
        public string Genre { get; set; }
        public string Chorus {get;set;}
        public string Verse { get; set; }   
        public string ShortDetails
        {
            get
            {
                var summary = StringExt.Truncate(Chorus, 125);
                if (summary == Chorus)
                    return Chorus;
                else
                    return summary + "...";
            }
        }
    }
}
