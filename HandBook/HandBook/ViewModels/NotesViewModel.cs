using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace HandBook.ViewModels
{
    class NotesViewModel
    {

        public bool tableExists ;

        public NotesViewModel()
        {
            refresh();
        }
     
        public List<Note> refresh()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                var notes = conn.Table<Note>().ToList();
                if (notes.Count <= 0)
                {
                    tableExists = true;
                }
                else
                {
                    tableExists = false;
                }
                return notes;
            }
            
        }

        public bool Update(Note note)
        {
            Note selectedNote = note;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                int rows = conn.Update(selectedNote);

                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
        }

        public bool DeleteItem(Note item)
        {
            var note = item;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                int rows = conn.Delete(note);

                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
         
        }
    }
}
