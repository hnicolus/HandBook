using HandBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using HandBook.Core.Functions;

namespace HandBook.ViewModels
{
    class NotesViewModel
    {
        public Note Note { get; set; }

        private List<Note> _notes;
        public List<Note> Notes
        {
            get { return _notes
                .Where( n => n.Deleted == false)
                .OrderByDescending( b => b.Id)
                .ToList(); }

            set { _notes = value; }
        }

        public NotesViewModel()
        {
            FetchList();
        }
        
        //Method to Returns data from database
        public void FetchList()
        {
            Notes = Crud.FetchNotes();

        }


        public bool TableExists()
        {
            return (Notes.Count <= 0);

        }

    }
}
