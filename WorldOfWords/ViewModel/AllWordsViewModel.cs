using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldOfWords.Infrastructure;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class AllWordsViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        Word selectedWord;

        public IWordService _wordService;
        public ObservableCollection<Word> Words { get; set; }

        public AllWordsViewModel(Frame menuFrame, IWordService wordService)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            Words = new ObservableCollection<Word>(_wordService.GetAllWords());
        }

        private RelayCommand moreCommand;
        public RelayCommand MoreCommand
        {
            get
            {
                return moreCommand ??
                  (moreCommand = new RelayCommand(obj =>
                  {
                      if(selectedWord != null)
                      {
                        int index = Words.IndexOf(selectedWord);
                        _menuFrame.Navigate(new WordInfo(_menuFrame, Words.ToList(), index));
                      }
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
                      if (selectedWord != null)
                      {
                          _wordService.Delete(SelectedWord.Id.ToString());
                          Words.RemoveAt(Words.IndexOf(selectedWord));
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
                      if (selectedWord != null)
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
                      if (selectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 0);
                          var word = _wordService.GetWord(SelectedWord.Id.ToString());
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          SelectedWord = word;
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
                      if (selectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 25);
                          var word = _wordService.GetWord(SelectedWord.Id.ToString());
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          SelectedWord = word;
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
                      if (selectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 50);
                          var word = _wordService.GetWord(SelectedWord.Id.ToString());
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          SelectedWord = word;
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
                      if (selectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 75);
                          var word = _wordService.GetWord(SelectedWord.Id.ToString());
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          SelectedWord = word;
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
                      if (selectedWord != null)
                      {
                          _wordService.SetKnow(SelectedWord.Id.ToString(), 100);
                          var word = _wordService.GetWord(SelectedWord.Id.ToString());
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
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
