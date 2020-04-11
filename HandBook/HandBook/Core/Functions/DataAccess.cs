using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandBook.Models;
using SQLite;

namespace HandBook.Core.Functions
{
    internal abstract class DataAccess
    {

        #region Notes CRUD Code Block

        public static Note GetNoteById(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                var note = conn.Table<Note>().Where(b => b.Id == id).SingleOrDefault();
                return note;
            }
        }
  
        public static List<Note> LoadNotes()
        {
            List<Note> notes;
            
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                notes = conn.Table<Note>().ToList();
                return notes;
            }
            
        }

        //Updates Note in Database
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
        //Delete Note in Database
        public static bool Delete(Note item)
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

        //Save Note in Database
        public static bool Save(Note note)
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


#pragma warning disable 1998
        public static async Task<bool> SaveAsync(Lyric lyric)
#pragma warning restore 1998
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Lyric>();
                int rows = conn.Insert(lyric);
                var totalCount = conn.Table<Lyric>().Count();
                return (rows > 0 || rows > totalCount);
            }
        }

        public static Lyric GetLyricById(int id)
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Lyric>();
                var lyric = conn.Table<Lyric>().SingleOrDefault(i => i.Id == id);
                return lyric;
            }
        }
        public static List<Lyric> LoadLyrics()
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

        public static bool Delete(Lyric item)
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
