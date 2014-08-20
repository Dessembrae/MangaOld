using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MangaCenterFromScratch.Model
{
    public class DataExtractor
    {
        public List<Manga> mangas = new List<Manga>();

        public void enumerateMainDirectory()
        {
            mangas.Clear();
            string[] list = Directory.GetDirectories(DefaultOptions.Instance.DirectoryPath);
            Console.WriteLine(list.Count());
            foreach (string d in list)
            {
                //Console.WriteLine(d);
                Manga manga = new Manga();
                manga.name = Path.GetFileName(d);

                string[] listOfChapters = Directory.GetDirectories(d + @"\");
                foreach (string chapter in listOfChapters)
                {
                    manga.mangaChapters.Add(chapter);
                }
                mangas.Add(manga);
            }
        }
        
    }
}
