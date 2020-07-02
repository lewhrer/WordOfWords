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
        public WordViewModel NewWord { get; set; }
        public IWordService _wordService;
        IUpdater updater;

        public CreateViewModel(Frame menuFrame, IWordService wordService, IUpdater updater)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            NewWord = new WordViewModel()
            {
                SourcePicture = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/Image.png"))
            };
            this.updater = updater;
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      NewWord.Picture = _wordService.FindImage();
                      NewWord.SourcePicture = _wordService.GetSourceImage(NewWord.Picture);
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
                      NewWord.Picture = null;
                      try
                      {
                          NewWord.SourcePicture = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/Image.png")); 
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
                          Example = NewWord.Example,
                          Name = NewWord.Name,
                          Picture = NewWord.Picture,
                          LastUpdate = new DateTime(2000, 1, 1),
                          Translate = NewWord.Translate,
                          Level = 0,
                          Priority = int.Parse(NewWord.WordPriority.Content.ToString()),
                      };

                      _wordService.Create(args);

                      MessageBox.Show("Sacsesfull created!");

                      _menuFrame.Navigate(new Create(_menuFrame, _wordService, updater));
                  }));
            }
        }

        private RelayCommand goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return goBackCommand ??
                  (goBackCommand = new RelayCommand(obj =>
                  {
                      updater.Update();
                      _menuFrame.GoBack();
                  }));
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
