using System;
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
using System.Collections.ObjectModel;

namespace MangaCenterFromScratch
{
    /// <summary>
    /// Interaction logic for LibraryTab.xaml
    /// </summary>
    public partial class LibraryTab : UserControl
    {
        DataExtractor extractor;
        ObservableCollection<Manga> mangaList;

        public LibraryTab()
        {
            InitializeComponent();

            extractor = new DataExtractor();
            UpdateList();

            MangaListView.ItemsSource = mangaList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //UpdateList();
        }

        private void UpdateList()
        {
            if (mangaList == null)
                mangaList = new ObservableCollection<Manga>();
            else
                mangaList.Clear();

            extractor.findAllMangas();
            foreach (Manga manga in extractor.mangas)
            {
                mangaList.Add(manga);
            }
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
