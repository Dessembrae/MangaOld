using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.ComponentModel;
using HtmlAgilityPack;

namespace MangaCenterFromScratch.Model
{
    public class MangaReaderDownloader : BaseMangaDownloader
    {

        public override void StartDownloading()
        {
            if (!initializationException)
            {
                Download();
            }
        }

        private void Download()
        {
            extractMangaName();
            while (extractMangaChapter())
            {
                //selecting div which holds image and link to next URL
                HtmlNode imgDiv = document.DocumentNode.SelectSingleNode("//img[@id='img']");

                string imgLink = extractImageLink(imgDiv);
                string nextURL = extractNextPageLink(imgDiv);
                string filename = String.Format(@"{0}\{1}\{2}{3}",
                    localMangaPath, currentChapterNumber, currentPageNumber, Path.GetExtension(imgLink));

                downloadImage(imgLink, filename);

                mangaURL = nextURL;
                initParser(mangaURL);

                currentPageNumber++;
            }
            InfoString = "Manga has ended.\n" + InfoString;
        }

        private string extractImageLink(HtmlNode imgDiv)
        {
            string imgLink = imgDiv.Attributes["src"].Value.ToString();
            return imgLink;
        }

        private string extractNextPageLink(HtmlNode imgDiv)
        {
            string nextUrl = "http://www.mangareader.net" + imgDiv.ParentNode.Attributes["href"].Value.ToString();
            return nextUrl;
        }

        private bool extractMangaChapter()
        {
            var chapterDiv = document.DocumentNode.SelectSingleNode("//img[@id='img']");
            string altAtt = null;
            if (chapterDiv == null)
                return false;
            else
                altAtt = chapterDiv.Attributes["alt"].Value.ToString(); 

            string chapterNumberGarbage = altAtt.Remove(0, mangaName.Count());
            string[] tempString = chapterNumberGarbage.Split('-');

            currentChapterNumber = "Chapter " + tempString[0].Trim();
            createDirectory(currentChapterNumber);
            return true;        
        }

        

        protected override void extractMangaName()
        {
            //getting manga name
            var nameDiv = document.DocumentNode.SelectNodes("//h2[@class='c2'] //a");
            mangaName = nameDiv.ElementAt(nameDiv.Count() - 1).InnerHtml;
            mangaName = mangaName.Remove(mangaName.Length - 6);
            localMangaPath = DefaultOptions.Instance.DirectoryPath + @"\" + mangaName;
            //******************

            //creating manga directory
            createDirectory();
            //************************
            //creating txt file for link to manga
            TextWriter tw = new StreamWriter(localMangaPath + @"\" + mangaName + ".txt", false);
            tw.WriteLine(mangaURL);
            tw.Close();
            //***********************************

        }

        
    }
}
