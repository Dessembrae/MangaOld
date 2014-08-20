using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.ComponentModel;

namespace MangaCenterFromScratch.Model
{
    public class MangaReaderDownloader : BaseMangaDownloader
    {
        

        public override void StartDownloading()
        {
            if (!initializationException)
            {
                gettingMangaName();

                bool continueLoop = true;
                while (continueLoop)
                {
                    if (gettingMangaChapterAndPage())
                        continueLoop = true;
                    else
                    {
                        continueLoop = false;
                        break;
                    }

                    var imgDiv = document.DocumentNode.SelectSingleNode("//img[@id='img']");
                    string imgLink = imgDiv.Attributes["src"].Value.ToString();
                    string nextUrl = "http://www.mangareader.net" + imgDiv.ParentNode.Attributes["href"].Value.ToString();                    
                    Console.WriteLine(nextUrl);

                    downloadImage(imgLink, directoryAndMangaPath + @"\" + chapterNumber + @"\" + pageNumber + Path.GetExtension(imgLink));
                    infoString = "Page " + pageNumber + " of " + chapterNumber + " chapter has been downloaded.\n" + infoString;
                    mangaPath = nextUrl;
                    initParser(mangaPath);

                    pageNumber++;
                }                                
            }
        }

        private bool gettingMangaChapterAndPage()
        {
            var chapterDiv = document.DocumentNode.SelectSingleNode("//img[@id='img']");
            string altAtt = "";
            try
            {
                altAtt = chapterDiv.Attributes["alt"].Value.ToString();
                string chapterNumberGarbage = altAtt.Remove(0, mangaName.Count());
                string[] tempString = chapterNumberGarbage.Split('-');

                chapterNumber = tempString[0].Trim();
                chapterNumber = "Chapter " + chapterNumber;
                createDirectory(chapterNumber);
                return true;
            }
            catch (NullReferenceException)
            {
                infoString = "Manga has probably ended(or an error occured).\n" + infoString;
                return false;
            }            
        }

        

        protected override void gettingMangaName()
        {
            //getting manga name
            var nameDiv = document.DocumentNode.SelectNodes("//h2[@class='c2'] //a");
            mangaName = nameDiv.ElementAt(nameDiv.Count() - 1).InnerHtml;
            mangaName = mangaName.Remove(mangaName.Length - 6);
            directoryAndMangaPath = DefaultOptions.Instance.DirectoryPath + @"\" + mangaName;
            //******************

            //creating manga directory
            createDirectory();
            //************************
            //creating txt file for link to manga
            TextWriter tw = new StreamWriter(directoryAndMangaPath + @"\" + mangaName + ".txt", false);
            tw.WriteLine(mangaPath);
            tw.Close();
            //***********************************

        }

        
    }
}
