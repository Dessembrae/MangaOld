using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MangaCenterFromScratch.Model
{
    public abstract class BaseMangaDownloader : ViewModelBase, INotifyPropertyChanged
    {
        protected HtmlWeb getHtmlWeb = new HtmlWeb();
        protected HtmlDocument document { get; set; }

        protected string localMangaPath { get; set; }
        protected string mangaURL { get; set; }

        protected string mangaName { get; set; }

        protected string currentChapterNumber { get; set; }
        protected int currentPageNumber { get; set; }
        
        private string _infoString;
        public string InfoString
        {
            get { return _infoString; }
            set { this._infoString = value; this.OnPropertyChanged("InfoString"); }
        }

        protected bool initializationException { get; set; }

        public void setProperties(string mangaURL)
        {
            try
            {
                initializationException = true;

                currentPageNumber = 1;
                this.mangaURL = mangaURL;
                initParser(this.mangaURL);

                initializationException = false;
            }
            catch (Exception)
            {
                MessageBox.Show("You didn't specify valid URL or download Directory.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        protected void downloadImage(string url, string filename)
        {
            using (WebClient wb = new WebClient())
            {
                wb.DownloadFile(url, filename);
                //InfoString = "Page " + currentPageNumber + " of " + currentChapterNumber + " chapter has been downloaded.\n" + InfoString;
                InfoString = String.Format("Page {0} of {1} chapter has been downloaded.\n{2}", currentPageNumber, currentChapterNumber, InfoString);
                //Console.WriteLine("Page " + currentPageNumber + " of " + currentChapterNumber + " chapter has been downloaded.");
            }
        }

        protected void initParser(string mangaPath)
        {
            document = getHtmlWeb.Load(mangaPath);
        }

        protected void createDirectory(string concat = "")
        {
            if (!String.IsNullOrEmpty(concat))
                concat = @"\" + concat;

            if (!Directory.Exists(localMangaPath + concat))
            {
                currentPageNumber = 1; //when creating new chapter directory, page number resets itself to zero.
                Directory.CreateDirectory(localMangaPath + concat);
            }
        }

        public abstract void StartDownloading();
        protected abstract void extractMangaName();
    }
}
