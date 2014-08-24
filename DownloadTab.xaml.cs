using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MangaCenterFromScratch
{
    /// <summary>
    /// Interaction logic for DownloadTab.xaml
    /// </summary>
    public partial class DownloadTab : UserControl
    {
        BaseMangaDownloader MangaDownloader;
        BackgroundWorker worker;

        public DownloadTab()
        {
            InitializeComponent();

            DefaultOptions.Instance.DirectoryPath = Directory.GetCurrentDirectory() + @"\MangaLibrary";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(DefaultOptions.Instance.DirectoryPath))
            {
                Directory.CreateDirectory(DefaultOptions.Instance.DirectoryPath);
            }

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
    }
}
