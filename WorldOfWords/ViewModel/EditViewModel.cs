using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class EditViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;

        public IWordService _wordService;
        IUpdater updater;
        public WordViewModel NewWord { get; set; }

        public EditViewModel(Frame menuFrame, IWordService wordService, string id, IUpdater updater)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            var word = _wordService.GetWord(id);
            this.updater = updater;
            NewWord = new WordViewModel(word.Priority)
            {
                Id = word.Id,
                Name = word.Name,
                Translate = word.Translate,
                Example = word.Example,
                Level = word.Level,
                LastUpdateDate = word.LastUpdate,
                Picture = word.Picture,
                SourcePicture = _wordService.GetSourceImage(word.Picture),
            };

            if(NewWord.Picture == null)
            {
                NewWord.SourcePicture = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/Image.png"));
            }
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
                      catch (Exception e)
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
                          Id = NewWord.Id.ToString(),
                          Example = NewWord.Example,
                          Name = NewWord.Name,
                          Picture = NewWord.Picture,
                          LastUpdate = NewWord.LastUpdateDate,
                          Translate = NewWord.Translate,
                          Level = NewWord.Level,
                          Priority = int.Parse(NewWord.WordPriority.Content.ToString()),
                      };

                      _wordService.Edit(args);
                      
                      MessageBox.Show("Sacsesfull edited!");
                      updater.Update();
                      _menuFrame.GoBack();
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
