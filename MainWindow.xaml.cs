using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MangaCenterFromScratch.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MangaCenterFromScratch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseMangaDownloader MangaDownloader;
        BackgroundWorker worker;
        DataExtractor extractor;
        ObservableCollection<Manga> mangaList;

        public MainWindow()
        {
            InitializeComponent();
            //DefaultOptions.Instance.DirectoryPath = @"C:\Users\Dess\Desktop\manga";

            DefaultOptions.Instance.DirectoryPath = Directory.GetCurrentDirectory() + @"\MangaLibrary";

            extractor = new DataExtractor();
            UpdateList();

            MangaListView.ItemsSource = mangaList;  
            
            infoBox.ScrollToEnd();
        }

        private void UpdateList()
        {
            if (mangaList == null)
                mangaList = new ObservableCollection<Manga>();
            else
                mangaList.Clear();

            extractor.enumerateMainDirectory();
            foreach (Manga manga in extractor.mangas)
            {
                mangaList.Add(manga);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MangaDownloader = new MangaReaderDownloader();
            infoBox.DataContext = MangaDownloader;
            MangaDownloader.setProperties(DownloadLink.Text);

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
            
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {            
            MangaDownloader.StartDownloading();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    LocationBox.Text = dlg.SelectedPath;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Manga manga = (Manga)MangaListView.SelectedItem;
            //Console.WriteLine(manga.name);
            //Console.WriteLine(manga.chaptersCount);
            throw new NotImplementedException();
        }
    }
}
