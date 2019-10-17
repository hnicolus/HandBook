using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandBook.Models
{
    public class Lock
    {
        [PrimaryKey, AutoIncrement]
        public int User_Id { get; set; }

        [MaxLength(30)]
        public string UserName { get; set; }
        public string email { get; set; }

        [MaxLength(30)]
        public static string Password { get; set; }
        public static bool ContentLocked { get; set; }
        public static string NickName { get; set; }
        public static string FavouriteColor { get; set; }
        public static string CityBorn { get; set; }
    }
}
