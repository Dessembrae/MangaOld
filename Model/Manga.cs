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
        public List<string> mangaChapters = new List<string>();
        public List<string> mangaPages = new List<string>();
        public int chaptersCount
        {
            get { return mangaChapters.Count(); }
        }

        public void setChapterList(string[] listOfChapters)
        {
            foreach (string chapter in listOfChapters)
            {
                mangaChapters.Add(chapter);
            }
        }

        public override string ToString()
        {
            return "Name of the manga: " + name + ", with " + chaptersCount + ((chaptersCount == 1) ? " chapter." : " chapters.");
        }
    }
}
