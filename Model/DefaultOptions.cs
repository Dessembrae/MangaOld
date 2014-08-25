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
        public static string _DirectoryPath { get; set; }
        public string DirectoryPath
        {
            get { return _DirectoryPath; }
            set { _DirectoryPath = value; OnPropertyChanged("DirectoryPath"); }
        }

        private static DefaultOptions _instance = new DefaultOptions();
        public static DefaultOptions Instance { get { return _instance; } } 

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
