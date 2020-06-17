using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        Uri menuUri;

        private RelayCommand allWordsCommand;
        public RelayCommand AllWordsCommand
        {
            get
            {
                return allWordsCommand ??
                  (allWordsCommand = new RelayCommand(obj =>
                  {
                      MenuUri = new Uri("AllWords.xaml", UriKind.Relative);
                  }));
            }
        }

        public Uri MenuUri
        {
            get { return menuUri; }
            set
            {
                menuUri = value;
                OnPropertyChanged("MenuUri");
            }
        }

        public MenuViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
