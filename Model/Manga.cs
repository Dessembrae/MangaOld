using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCenterFromScratch.Model
{
    public class Manga
    {
        public string name { get; set; }
        public List<string> mangaChapters { get; set; }
        public List<string> mangaPages { get; set; }
        public int chaptersCount
        {
            get { return mangaChapters.Count(); }
        }

        public Manga()
        {
            mangaChapters = new List<string>();
            mangaPages = new List<string>();
        }

        public override string ToString()
        {
            return "Name of the manga: " + name + ", with " + chaptersCount + ((chaptersCount == 1) ? " chapter." : " chapters.");
        }
    }
}
