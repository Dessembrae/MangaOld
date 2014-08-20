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
    public abstract class BaseMangaDownloader : INotifyPropertyChanged
    {
        protected HtmlWeb getHtmlWeb { get; set; }
        protected HtmlDocument document { get; set; }
        protected string directoryAndMangaPath { get; set; }
        protected string mangaPath { get; set; }
        protected string mangaName { get; set; }
        protected string chapterNumber { get; set; }
        protected int pageNumber { get; set; }
        protected bool initializationException { get; set; }
        public String InfoString;
        public String infoString
        {
            get { return InfoString; }
            set { InfoString = value; this.OnPropertyChanged("infoString"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void setProperties(string mangaPath)
        {
            try
            {
                initializationException = true;

                pageNumber = 1;
                this.mangaPath = mangaPath;
                getHtmlWeb = new HtmlWeb();
                initParser(this.mangaPath);

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
                
                Console.WriteLine("Page " + pageNumber + " of " + chapterNumber + " chapter has been downloaded.");
            }
        }

        protected void initParser(string mangaPath)
        {
            document = getHtmlWeb.Load(mangaPath);
        }

        protected void createDirectory()
        {
            if (!Directory.Exists(directoryAndMangaPath))
            {
                Directory.CreateDirectory(directoryAndMangaPath);
            }
        }

        protected void createDirectory(string concat)
        {
            if (!Directory.Exists(directoryAndMangaPath + @"\" + concat))
            {
                pageNumber = 1; //when creating new chapter directory, page number resets itself to zero.
                Directory.CreateDirectory(directoryAndMangaPath + @"\" + concat);
            }
        }

        public abstract void StartDownloading();
        protected abstract void gettingMangaName();

        

    }
}
