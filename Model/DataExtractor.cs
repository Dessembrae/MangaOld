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
        private string directory = DefaultOptions.Instance.DirectoryPath;

        public void findAllMangas()
        {
            mangas.Clear();
            string[] mangaList = Directory.GetDirectories(directory);
            foreach (string title in mangaList)
            {
                Manga manga = new Manga();
                manga.name = Path.GetFileName(title);

                string[] listOfChapters = Directory.GetDirectories(title + @"\");
                manga.setChapterList(listOfChapters);
                mangas.Add(manga);
            }
        }
        
    }
}
