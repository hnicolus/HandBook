using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HandBook.Models;
using SQLite;

namespace HandBook.Core.Functions
{
    class Crud
    {

        #region Notes CRUD Code Block

        public static List<Note> FetchNotes()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                var notes = conn.Table<Note>().ToList();

                return notes;
            }

        }

        //Updates an item in Database
        public static bool Update(Note note)
        {
            Note selectedNote = note;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                int rows = conn.Update(selectedNote);

                return (rows > 0);
            }
        }


        public static bool DeleteItem(Note item)
        {
            var note = item;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                var totalCount = conn.Table<Note>().Count();

                int rows = conn.Delete(note);
                return (rows < totalCount);

            }

        }

        public static async Task<bool> SaveAsync(Note note)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                int rows = conn.Insert(note);
                var totalCount = conn.Table<Note>().Count();
                return (rows > 0 || rows > totalCount);
            }
        }
        #endregion

        #region Lyrics  CRUD Code Block


        public static async Task<bool> SaveAsync(Lyric lyric)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Lyric>();
                int rows = conn.Insert(lyric);
                var totalCount = conn.Table<Lyric>().Count();
                return (rows > 0 || rows > totalCount);
            }
        }

        public static List<Lyric> FetchLyrics()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Lyric>();
                var lyrics = conn.Table<Lyric>().ToList();

                return lyrics;
            }

        }

        //Updates an item in Database
        public static bool Update(Lyric lyric)
        {
          
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Lyric>();
                int rows = conn.Update(lyric);

                return (rows > 0);
            }
        }


        public static bool DeleteItem(Lyric item)
        {
           
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Lyric>();
                var totalCount = conn.Table<Lyric>().Count();

                int rows = conn.Delete(item);
                return (rows < totalCount);

            }

        }

        #endregion
    }
}
