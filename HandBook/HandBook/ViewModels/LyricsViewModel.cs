using System;
using System.Collections.Generic;
using System.Text;
using HandBook.Core.Functions;
using HandBook.Models;

namespace HandBook.ViewModels
{
    class LyricsViewModel
    {
        public Lyric Lyric { get; set; }
        public List<Lyric> Lyrics { get; set; }

        public void FetchList()
        {
            Lyrics = Crud.FetchLyrics();


        }

        public bool TableExists()
        {
            return (Lyrics.Count <= 0);

        }

    }
}
