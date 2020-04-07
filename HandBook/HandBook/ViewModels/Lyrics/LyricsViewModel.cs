using System.Collections.Generic;
using System.Linq;
using HandBook.Core.Functions;
using HandBook.Models;

namespace HandBook.ViewModels
{
    class LyricsViewModel
    {
        public LyricsViewModel()
        {
            FetchList();
           
        }
        public Lyric Lyric { get; set; }
        public List<Lyric> Lyrics { get;set; }
        public bool TableExist { get; set; }
        public void FetchList() => Lyrics = DataAccess.LoadLyrics().OrderBy(b => b.Id).ToList();

        public void TableExists()
        {
            TableExist = (Lyrics.Count <= 0);
        } 

        public bool Delete( Lyric item)
        {
            var isDelete = DataAccess.Delete(item);
            return isDelete;
        }
    }
}
