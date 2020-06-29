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
using WorldOfWords.Model;

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

        #region trainCommand

        private RelayCommand trainAllCommand;
        public RelayCommand TrainAllCommand
        {
            get
            {
                return trainAllCommand ??
                  (trainAllCommand = new RelayCommand(obj =>
                  {
                      TrainCommand(_wordService.GetTrainAllWords(), "train all word", "All", "All words");
                  }));
            }
        }

        private RelayCommand train0Command;
        public RelayCommand Train0Command
        {
            get
            {
                return train0Command ??
                  (train0Command = new RelayCommand(obj =>
                  {
                      TrainCommand(_wordService.GetTrainNotStudiedWords(), "train 0 word", "0", "0 words");
                  }));
            }
        }

        private RelayCommand train25Command;
        public RelayCommand Train25Command
        {
            get
            {
                return train25Command ??
                  (train25Command = new RelayCommand(obj =>
                  {
                      TrainCommand(_wordService.GetTrainAlmostAcquaintedWords(), "train 25 word", "25", "25 words");
                  }));
            }
        }

        private RelayCommand train50Command;
        public RelayCommand Train50Command
        {
            get
            {
                return train50Command ??
                  (train50Command = new RelayCommand(obj =>
                  {
                      TrainCommand(_wordService.GetTrainAcquaintedWords(), "train 50 word", "50", "50 words");
                  }));
            }
        }

        private RelayCommand train75Command;
        public RelayCommand Train75Command
        {
            get
            {
                return train75Command ??
                  (train75Command = new RelayCommand(obj =>
                  {
                        TrainCommand(_wordService.GetTrainAlmostStudiedWords(), "train 75 word", "75", "75 words");
                  }));
            }
        }

        private RelayCommand knowTrainCommand;
        public RelayCommand KnowTrainCommand
        {
            get
            {
                return knowTrainCommand ??
                  (knowTrainCommand = new RelayCommand(obj =>
                  {
                      TrainCommand(_wordService.GetTrainStudiedWords(), "train know word", "100", "know words");
                  }));
            }
        }
        #endregion

        #region knowWordsCommand

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

        #endregion

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

        private void TrainCommand(List<Word> words, string trainPaage, string listPageId, string listPageName)
        {
            try
            {
                if (words.Any())
                {
                    var viewModel = ((ListOfWords)_menuFrame.Content).ViewModel;

                    _menuFrame.Navigate(new WordInfo(_menuFrame, _wordService, words, viewModel, trainPaage));
                }
                else
                {
                    MessageBox.Show("Array is empty");
                }
            }
            catch (Exception)
            {
                try
                {
                    if (words.Any())
                    {
                        var viewModel = ((WordInfo)_menuFrame.Content).ViewModel;
                        _menuFrame.Navigate(new WordInfo(_menuFrame, _wordService, words, viewModel, trainPaage));
                    }
                    else
                    {
                        MessageBox.Show("Array is empty");
                    }
                }
                catch (Exception)
                {
                    _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, listPageId, listPageName));
                }
            }
        }
    }
}
