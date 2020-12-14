using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorldOfWords.View;
using WorldOfWords.Model;

namespace WorldOfWords.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel()
        {
        }

        public RelayCommand ThemesCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Menu.Frame.Navigate(new ListOfThemes());
                });
            }
        }
        public RelayCommand AllWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new ListOfWords("All", Application.Current.Resources["AllWords"].ToString(),
                          Application.Current.Resources["TrainingAllWords"].ToString()));
                  });
            }
        }

        public RelayCommand SettingsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new Settings());
                  });
            }
        }
        public RelayCommand HelpCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    View.Menu.Frame.Navigate(new Help());
                });
            }
        }

        #region trainCommand

        public RelayCommand TrainAllWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainAllWords(), 
                          Application.Current.Resources["TrainingAllWords"].ToString(), "All", Application.Current.Resources["AllWords"].ToString());
                  });
            }
        }

        public RelayCommand TrainOfUnknownWords
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainWords(0, Resource.getInstance().Level.First, 
                          Resource.getInstance().TrainDate.Unknown), Application.Current.Resources["TrainingUnknownWords"].ToString(), "0", Application.Current.Resources["UnknownWords"].ToString());
                  });
            }
        }

        public RelayCommand TrainNotMemorizedWords
        {
            get
            {
                return  new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainWords(Resource.getInstance().Level.First, 
                          Resource.getInstance().Level.Second, Resource.getInstance().TrainDate.NotMemorized),
                          Application.Current.Resources["TrainingNotMemorizedWords"].ToString(), "33", Application.Current.Resources["NotMemorizedWords"].ToString());
                  });
            }
        }

        public RelayCommand TrainMemorizedWords
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      TrainCommand(Resource.getInstance().WordService.GetTrainWords(Resource.getInstance().Level.Second, 
                          Resource.getInstance().Level.Third, Resource.getInstance().TrainDate.Memorized),
                          Application.Current.Resources["TrainingMemorizedWords"].ToString(), "66", Application.Current.Resources["MemorizedWords"].ToString());
                  });
            }
        }

        public RelayCommand TrainLearnedWords
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                        TrainCommand(Resource.getInstance().WordService.GetTrainWords(Resource.getInstance().Level.Third, 
                            100, Resource.getInstance().TrainDate.Learned), Application.Current.Resources["TrainingLearnedWords"].ToString(), 
                            "100", Application.Current.Resources["LearnedWords"].ToString());
                  });
            }
        }
        #endregion

        #region knowWordsCommand

        public RelayCommand UnknownWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new ListOfWords("0", Application.Current.Resources["UnknownWords"].ToString(), 
                          Application.Current.Resources["TrainingUnknownWords"].ToString()));
                  });
            }
        }

        public RelayCommand NotMemorizedWordsCommand
        {
            get
            {
                return  new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new ListOfWords("33", Application.Current.Resources["NotMemorizedWords"].ToString(),
                          Application.Current.Resources["TrainingNotMemorizedWords"].ToString()));
                  });
            }
        }

        public RelayCommand MemorizedWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new ListOfWords("66", Application.Current.Resources["MemorizedWords"].ToString(),
                          Application.Current.Resources["TrainingMemorizedWords"].ToString()));
                  });
            }
        }
        public RelayCommand LearnedWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      View.Menu.Frame.Navigate(new ListOfWords("100", Application.Current.Resources["LearnedWords"].ToString(),
                          Application.Current.Resources["TrainingLearnedWords"].ToString()));
                  });
            }
        }

        #endregion

        public RelayCommand CreateCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      try
                      {
                              var viewModel = ((ListOfWords)View.Menu.Frame.Content).ViewModel;
                          View.Menu.Frame.Navigate(new Create(viewModel));
                      }
                      catch (Exception)
                      {
                          try
                          {
                                  var viewModel = ((WordInfo)View.Menu.Frame.Content).ViewModel;
                              View.Menu.Frame.Navigate(new Create(viewModel));
                          }
                          catch (Exception)
                          {
                              View.Menu.Frame.Navigate(new Create());
                          }
                      }
                  });
            }
        }

        public void TrainCommand(List<Word> words, string trainPage, string listPageId, string listPageName)
        {
            try
            {
                if (words.Any())
                {
                    var viewModel = ((ListOfWords)View.Menu.Frame.Content).ViewModel;

                    View.Menu.Frame.Navigate(new WordInfo(words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList(), trainPage, viewModel));
                }
                else
                {
                    (new Warning(Application.Current.Resources["NoWordsInThisCategory"].ToString())).ShowDialog();
                }
            }
            catch (Exception)
            {
                try
                {
                    if (words.Any())
                    {
                        var viewModel = ((WordInfo)View.Menu.Frame.Content).ViewModel;
                        View.Menu.Frame.Navigate(new WordInfo(words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList(), trainPage, viewModel));
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["NoWordsInThisCategory"].ToString())).ShowDialog();
                    }
                }
                catch (Exception)
                {
                    View.Menu.Frame.Navigate(new WordInfo(words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList(), trainPage));
                }
            }
        }
    }
}
