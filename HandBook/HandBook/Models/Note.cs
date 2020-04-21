using HandBook.Core.Functions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Models
{
    public class Note : Post
    {
        #region Properties
        public string Body { get; set; }
        public string ShortDetails 
        {
            get
            {
                var summary = StringExt.Truncate(Body, 160);
                if (summary == Body)
                    return Body;
                else
                    return summary+"...";
            }
        }
        #endregion

        #region Constructor
        #endregion

        #region Methods
        #endregion
    }
}
