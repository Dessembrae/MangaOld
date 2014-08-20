using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCenterFromScratch.Model
{
    public sealed class DefaultOptions : INotifyPropertyChanged
    {
        private DefaultOptions()
        {

        }

        public static string _DirectoryPath { get; set; }
        public string DirectoryPath
        {
            get { return _DirectoryPath; }
            set { _DirectoryPath = value; OnPropertyChanged("DirectoryPath"); }
        }

        private static DefaultOptions _Instance = new DefaultOptions();
        public static DefaultOptions Instance { get { return _Instance; } } 

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
