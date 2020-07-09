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
using System.Runtime.Serialization.Json;
using System.IO;

namespace WorldOfWords.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public MenuViewModel()
        {
        }

        private RelayCommand allWordsCommand;
        public RelayCommand AllWordsCommand
        {
            get
            {
                return allWordsCommand ??
                  (allWordsCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MenuFrame.Navigate(new ListOfWords("All", "Всі слова"));
                  }));
            }
        }

        private RelayCommand settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MenuFrame.Navigate(new Settings());
                  }));
            }
        }

        #region trainCommand

        private RelayCommand trainAllWordsCommand;
        public RelayCommand TrainAllWordsCommand
        {
            get
            {
                return trainAllWordsCommand ??
                  (trainAllWordsCommand = new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainAllWords(), "Тренування всіх слів", "All", "Всі слова");
                  }));
            }
        }

        private RelayCommand trainOfUnknownWords;
        public RelayCommand TrainOfUnknownWords
        {
            get
            {
                return trainOfUnknownWords ??
                  (trainOfUnknownWords = new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainWords(0, Resource.getInstance().Level.First, 
                          Resource.getInstance().TrainDate.Unknown), "Тренування невідомих слів", "0", "Невідомі слова");
                  }));
            }
        }

        private RelayCommand trainNotMemorizedWords;
        public RelayCommand TrainNotMemorizedWords
        {
            get
            {
                return trainNotMemorizedWords ??
                  (trainNotMemorizedWords = new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainWords(Resource.getInstance().Level.First, 
                          Resource.getInstance().Level.Second, Resource.getInstance().TrainDate.NotMemorized), 
                          "Тренування не запам'ятованих слів", "33", "Незапам'ятовані слова");
                  }));
            }
        }

        private RelayCommand trainMemorizedWords;
        public RelayCommand TrainMemorizedWords
        {
            get
            {
                return trainMemorizedWords ??
                  (trainMemorizedWords = new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainWords(Resource.getInstance().Level.Second, 
                          Resource.getInstance().Level.Third, Resource.getInstance().TrainDate.Memorized), 
                          "Тренування зам'ятованих слів", "66", "Запам'ятовані слова");
                  }));
            }
        }

        private RelayCommand trainLearnedWords;
        public RelayCommand TrainLearnedWords
        {
            get
            {
                return trainLearnedWords ??
                  (trainLearnedWords = new RelayCommand(obj =>
                  {
                        TrainCommand(Resource.getInstance().WordService.GetTrainWords(Resource.getInstance().Level.Third, 
                            100, Resource.getInstance().TrainDate.Learned), "Тренування вивчених слів", "100", "Вивчені слова");
                  }));
            }
        }
        #endregion

        #region knowWordsCommand

        private RelayCommand unknownWordsCommand;
        public RelayCommand UnknownWordsCommand
        {
            get
            {
                return unknownWordsCommand ??
                  (unknownWordsCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MenuFrame.Navigate(new ListOfWords("0", "Невідомі слова"));
                  }));
            }
        }

        private RelayCommand notMemorizedWordsCommand;
        public RelayCommand NotMemorizedWordsCommand
        {
            get
            {
                return notMemorizedWordsCommand ??
                  (notMemorizedWordsCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MenuFrame.Navigate(new ListOfWords("33", "Не запам'ятовані слова"));
                  }));
            }
        }

        private RelayCommand memorizedWordsCommand;
        public RelayCommand MemorizedWordsCommand
        {
            get
            {
                return memorizedWordsCommand ??
                  (memorizedWordsCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MenuFrame.Navigate(new ListOfWords("66", "Запам'ятовані слова"));
                  }));
            }
        }

        private RelayCommand learnedWordsCommand;
        public RelayCommand LearnedWordsCommand
        {
            get
            {
                return learnedWordsCommand ??
                  (learnedWordsCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MenuFrame.Navigate(new ListOfWords("100", "Вивчені слова"));
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
                      try
                      {
                              var viewModel = ((ListOfWords)Resource.getInstance().MenuFrame.Content).ViewModel;
                          Resource.getInstance().MenuFrame.Navigate(new Create(viewModel));
                      }
                      catch (Exception)
                      {
                          try
                          {
                                  var viewModel = ((WordInfo)Resource.getInstance().MenuFrame.Content).ViewModel;
                              Resource.getInstance().MenuFrame.Navigate(new Create(viewModel));
                          }
                          catch (Exception)
                          {
                              Resource.getInstance().MenuFrame.Navigate(new ListOfWords("All", "Всі слова"));
                          }
                      }
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
                    var viewModel = ((ListOfWords)Resource.getInstance().MenuFrame.Content).ViewModel;

                    Resource.getInstance().MenuFrame.Navigate(new WordInfo(words.Select(x => new WordViewModel(x)).ToList(), viewModel, trainPaage));
                }
                else
                {
                    MessageBox.Show("Список пустий");
                }
            }
            catch (Exception)
            {
                try
                {
                    if (words.Any())
                    {
                        var viewModel = ((WordInfo)Resource.getInstance().MenuFrame.Content).ViewModel;
                        Resource.getInstance().MenuFrame.Navigate(new WordInfo(words.Select(x => new WordViewModel(x)).ToList(), viewModel, trainPaage));
                    }
                    else
                    {
                        MessageBox.Show("Список пустий");
                    }
                }
                catch (Exception)
                {
                    Resource.getInstance().MenuFrame.Navigate(new ListOfWords(listPageId, listPageName));
                }
            }
        }
    }
}
