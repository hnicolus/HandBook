using HandBook.Core.Functions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Models
{
    public class Note : Post
    {
        public string Body { get; set; }
        public string ShortDetails 
        {
            get
            {
                var summary = StringExt.Truncate(Body, 125);
                if (summary == Body)
                    return Body;
                else
                    return summary+"...";
            }
        }
    }
}
