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
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetAllWords()));
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
                      _menuFrame.Navigate(new WordInfo(_menuFrame, _wordService, _wordService.GetAllWords()));
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
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetNotStudiedWords()));
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
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetAlmostAcquaintedWords()));
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
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetAcquaintedWords()));
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
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetAlmostStudiedWords()));
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
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetStudiedWords()));
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
