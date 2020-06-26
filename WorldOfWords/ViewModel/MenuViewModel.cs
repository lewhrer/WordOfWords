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
using WorldOfWords.Infrastructure.Services;

namespace WorldOfWords.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        public IWordService _wordService;

        public MenuViewModel(Frame menuFrame, IWordService wordService)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
        }

        private RelayCommand allWordsCommand;
        public RelayCommand AllWordsCommand
        {
            get
            {
                return allWordsCommand ??
                  (allWordsCommand = new RelayCommand(obj =>
                  {
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, "All", "all words"));
                  }));
            }
        }

        private RelayCommand trainCommand;
        public RelayCommand TrainCommand
        {
            get
            {
                return trainCommand ??
                  (trainCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                            var viewModel = ((ListOfWords)_menuFrame.Content).ViewModel;
                            _menuFrame.Navigate(new WordInfo(_menuFrame, _wordService, _wordService.GetAllWords(), viewModel));
                      }
                      catch(Exception)
                      {
                          try
                          {
                              var viewModel = ((WordInfo)_menuFrame.Content).ViewModel;
                              _menuFrame.Navigate(new WordInfo(_menuFrame, _wordService, _wordService.GetAllWords(), viewModel));
                          }
                          catch(Exception)
                          {
                                _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, "All", "All words"));
                          }
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
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, "0", "0 words"));
                  }));
            }
        }

        private RelayCommand know25WordsCommand;
        public RelayCommand Know25WordsCommand
        {
            get
            {
                return know25WordsCommand ??
                  (know25WordsCommand = new RelayCommand(obj =>
                  {
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, "25", "25 words"));
                  }));
            }
        }

        private RelayCommand know50WordsCommand;
        public RelayCommand Know50WordsCommand
        {
            get
            {
                return know50WordsCommand ??
                  (know50WordsCommand = new RelayCommand(obj =>
                  {
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, "50", "50 words"));
                  }));
            }
        }

        private RelayCommand know75WordsCommand;
        public RelayCommand Know75WordsCommand
        {
            get
            {
                return know75WordsCommand ??
                  (know75WordsCommand = new RelayCommand(obj =>
                  {
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, "75", "75 words"));
                  }));
            }
        }

        private RelayCommand knowWordsCommand;
        public RelayCommand KnowWordsCommand
        {
            get
            {
                return knowWordsCommand ??
                  (knowWordsCommand = new RelayCommand(obj =>
                  {
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, "100", "100 words"));
                  }));
            }
        }

        private RelayCommand createCommand;
        public RelayCommand CreateCommand
        {
            get
            {
                return createCommand ??
                  (createCommand = new RelayCommand(obj =>
                  {
                      _menuFrame.Navigate(new Create(_menuFrame, _wordService));
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
