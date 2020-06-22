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
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class WordInfoViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        Word selectedWord;
        Image picture;

        public IWordService _wordService;
        public ObservableCollection<Word> Words { get; set; }

        public WordInfoViewModel(Frame menuFrame, IWordService wordService, List<Word> words, int indexWord, Image picture)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            Words = new ObservableCollection<Word>(words);
            SelectedWord = Words[indexWord];
            this.picture = picture;
            if(SelectedWord.Picture != null)
            {
                picture.Source = _wordService.GetSourceImage(SelectedWord.Picture);
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
                      if (SelectedWord != null)
                      {
                          _wordService.Delete(SelectedWord.Id.ToString());
                          Words.RemoveAt(Words.IndexOf(SelectedWord));
                      }
                  }));
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          _menuFrame.Navigate(new Edit(_menuFrame, SelectedWord.Id.ToString()));
                      }
                  }));
            }
        }

        private RelayCommand know0Command;
        public RelayCommand Know0Command
        {
            get
            {
                return know0Command ??
                  (know0Command = new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 0);
                          Words.RemoveAt(Words.IndexOf(SelectedWord));
                      }
                  }));
            }
        }

        private RelayCommand know25Command;
        public RelayCommand Know25Command
        {
            get
            {
                return know25Command ??
                  (know25Command = new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 25);
                          Words.RemoveAt(Words.IndexOf(SelectedWord));
                      }
                  }));
            }
        }

        private RelayCommand know50Command;
        public RelayCommand Know50Command
        {
            get
            {
                return know50Command ??
                  (know50Command = new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 50);
                          Words.RemoveAt(Words.IndexOf(SelectedWord));
                      }
                  }));
            }
        }

        private RelayCommand know75Command;
        public RelayCommand Know75Command
        {
            get
            {
                return know75Command ??
                  (know75Command = new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 75);
                          Words.RemoveAt(Words.IndexOf(SelectedWord));
                      }
                  }));
            }
        }

        private RelayCommand know100Command;
        public RelayCommand Know100Command
        {
            get
            {
                return know100Command ??
                  (know100Command = new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 100);
                          var word = _wordService.GetWord(SelectedWord.Id.ToString());
                          Words[Words.IndexOf(SelectedWord)] = word;
                          SelectedWord = word;
                      }
                  }));
            }
        }

        public Word SelectedWord
        {
            get { return selectedWord; }
            set
            {
                selectedWord = value;
                OnPropertyChanged("SelectedWord");
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
