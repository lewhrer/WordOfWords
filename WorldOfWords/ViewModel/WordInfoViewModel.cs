﻿using System;
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
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class WordInfoViewModel : INotifyPropertyChanged, IUpdater
    {
        Frame _menuFrame;
        WordViewModel selectedWord;
        int indexSelectedWord;
        int numberOfWord;
        string namePage;
        int totalCount;

        IUpdater updater;
        public IWordService _wordService;
        public ObservableCollection<WordViewModel> Words { get; set; }

        public WordInfoViewModel(Frame menuFrame, IWordService wordService, List<WordViewModel> words, IUpdater updater, string namePage, int indexWord)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            Words = new ObservableCollection<WordViewModel>(words);
            this.updater = updater;
            NamePage = namePage;
            IndexSelectedWord = indexWord + 1;
            SelectedWord = Words[indexWord];
            TotalCount = Words.Count;
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
                          TotalCount--;
                      }
                  }));
            }
        }

        private RelayCommand nextCommand;
        public RelayCommand NextCommand
        {
            get
            {
                return nextCommand ??
                  (nextCommand = new RelayCommand(obj =>
                  {
                      if (IndexSelectedWord != Words.Count)
                      {
                          SelectedWord = null;
                          SelectedWord = Words[IndexSelectedWord++];
                      }
                  }));
            }
        }

        private RelayCommand showPictureCommand;
        public RelayCommand ShowPictureCommand
        {
            get
            {
                return showPictureCommand ??
                  (showPictureCommand = new RelayCommand(obj =>
                  {
                      SelectedWord.SourcePicture = SelectedWord.SourcePictureCorectly;
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
                      _menuFrame.GoBack();
                      updater.Update();
                  }));
            }
        }

        private RelayCommand previouslyCommand;
        public RelayCommand PreviouslyCommand
        {
            get
            {
                return previouslyCommand ??
                  (previouslyCommand = new RelayCommand(obj =>
                  {
                      if (IndexSelectedWord - 1 != 0)
                      {
                          SelectedWord = null;
                          IndexSelectedWord = IndexSelectedWord - 2;
                          SelectedWord = Words[IndexSelectedWord++];
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
                          _menuFrame.Navigate(new Edit(_menuFrame, _wordService, SelectedWord.Id.ToString(), this));
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
                          var word = new WordViewModel(_wordService.GetWord(SelectedWord.Id.ToString()));
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          if (index + 1 != Words.Count)
                          {
                              ++IndexSelectedWord;
                              SelectedWord = Words[++index];
                          }
                          else
                          {
                              SelectedWord = null;
                              SelectedWord = Words[index];
                          }
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
                          var word = new WordViewModel(_wordService.GetWord(SelectedWord.Id.ToString()));
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          if (index + 1 != Words.Count)
                          {
                              ++IndexSelectedWord;
                              SelectedWord = Words[++index];
                          }
                          else
                          {
                              SelectedWord = null;
                              SelectedWord = Words[index];
                          }
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
                          var word = new WordViewModel(_wordService.GetWord(SelectedWord.Id.ToString()));
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          if (index + 1 != Words.Count)
                          {
                              ++IndexSelectedWord;
                              SelectedWord = Words[++index];
                          }
                          else
                          {
                              SelectedWord = null;
                              SelectedWord = Words[index];
                          }
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
                          var word = new WordViewModel(_wordService.GetWord(SelectedWord.Id.ToString()));
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          if (index + 1 != Words.Count)
                          {
                              ++IndexSelectedWord;
                              SelectedWord = Words[++index];
                          }
                          else
                          {
                              SelectedWord = null;
                              SelectedWord = Words[index];
                          }
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
                          var word = new WordViewModel(_wordService.GetWord(SelectedWord.Id.ToString()));
                          int index = Words.IndexOf(SelectedWord);
                          Words.RemoveAt(index);
                          Words.Insert(index, word);
                          if (index + 1 != Words.Count)
                          {
                              ++IndexSelectedWord;
                              SelectedWord = Words[++index];
                          }
                          else
                          {
                              SelectedWord = null;
                              SelectedWord = Words[index];
                          }
                      }
                  }));
            }
        }

        public WordViewModel SelectedWord
        {
            get { return selectedWord; }
            set
            {
                selectedWord = value;
                OnPropertyChanged("SelectedWord");
            }
        }

        public string NamePage
        {
            get { return namePage; }
            set
            {
                namePage = value;
                OnPropertyChanged("NamePage");
            }
        }

        public int IndexSelectedWord
        {
            get { return indexSelectedWord; }
            set
            {
                indexSelectedWord = value;
                OnPropertyChanged("IndexSelectedWord");
            }
        }

        public int NumberOfWord
        {
            get { return numberOfWord; }
            set
            {
                numberOfWord = value;
                OnPropertyChanged("NumberOfWord");
            }
        }

        public int TotalCount
        {
            get { return totalCount; }
            set
            {
                totalCount = value;
                OnPropertyChanged("TotalCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Update()
        {
            var word = new WordViewModel(_wordService.GetWord(SelectedWord.Id.ToString()));
            int index = Words.IndexOf(SelectedWord);
            Words.RemoveAt(index);
            Words.Insert(index, word);
            SelectedWord = null;
            SelectedWord = Words[index];
            TotalCount = Words.Count;
        }
    }
}
