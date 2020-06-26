using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class CreateViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        Image picture;
        byte[] pictureInBytes;

        public IWordService _wordService;

        private string name;
        private string translate;
        private string example;

        public CreateViewModel(Frame menuFrame, IWordService wordService, Image picture)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            this.picture = picture;
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      pictureInBytes = _wordService.FindImage();
                      picture.Source = _wordService.GetSourceImage(pictureInBytes);
                  }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      pictureInBytes = null;
                      try
                      {
                          picture.Source = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/temp.jpg")); 
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      var args = new WordArgs()
                      {
                          Example = Example,
                          Name = Name,
                          Picture = pictureInBytes,
                          LastUpdate = DateTime.Now,
                          TranslateName = Translate,
                          Level = 0,
                      };

                      _wordService.Create(args);

                      MessageBox.Show("Sacsesfull created!");

                      _menuFrame.Navigate(new Create(_menuFrame, _wordService));
                  }));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Translate
        {
            get { return translate; }
            set
            {
                translate = value;
                OnPropertyChanged("Translate");
            }
        }

        public string Example
        {
            get { return example; }
            set
            {
                example = value;
                OnPropertyChanged("Example");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
