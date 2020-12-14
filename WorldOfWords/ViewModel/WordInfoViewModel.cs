using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class WordInfoViewModel : BaseViewModel, IUpdater
    {
        WordViewModel selectedWord;
        int indexSelectedWord;
        int numberOfWord;
        string namePage;
        int totalCount;

        IUpdater updater;
        public ObservableCollection<WordViewModel> Words { get; set; }

        public WordInfoViewModel(List<WordViewModel> words, IUpdater updater, string namePage, int indexWord)
        {
            Words = new ObservableCollection<WordViewModel>(words);
            this.updater = updater;
            NamePage = namePage;
            IndexSelectedWord = indexWord + 1;
            SelectedWord = Words[indexWord];
            TotalCount = Words.Count;
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          Resource.getInstance().WordService.Delete(SelectedWord.Id.ToString());
                          Words.RemoveAt(Words.IndexOf(SelectedWord));
                          TotalCount--;
                          (new Warning(Application.Current.Resources["WordWasDeletedSuccessfully"].ToString())).ShowDialog();
                          if (Words.Count == 0)
                          {
                              View.Menu.Frame.NavigationService.GoBack();
                              if (updater != null)
                                  updater.Update();
                          }
                          else if (Words.Count == IndexSelectedWord - 1)
                          {
                              IndexSelectedWord--;
                              SelectedWord = Words[IndexSelectedWord - 1];
                          }
                          else
                          {
                              SelectedWord = Words[IndexSelectedWord - 1];
                          }
                      }
                  });
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
                          SelectedWord = Words[IndexSelectedWord++];
                      }
                      else
                      {
                          (new Warning(Application.Current.Resources["EndOfList"].ToString())).ShowDialog();
                      }
                  }));
            }
        }

        public RelayCommand ShowPictureCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      SelectedWord.SourcePicture = SelectedWord.SourcePictureCorectly;
                  });
            }
        }

        public RelayCommand MouseDownOnPictureCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      if (SelectedWord.SourcePicture != null)
                      {
                          View.Menu.Frame.Navigate(new PhotoViewer(SelectedWord.SourcePicture));
                      }
                  });
            }
        }

        public RelayCommand MouseDownOnHidedPictureCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      SelectedWord.HidedPicture = null;
                      SelectedWord.SourcePicture = SelectedWord.SourcePictureCorectly;
                  });
            }
        }

        public RelayCommand MouseDownOnTranslateCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      SelectedWord.HidedTranslate = SelectedWord.Translate;
                  });
            }
        }

        public RelayCommand MouseDownOnExampleCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      SelectedWord.Example = SelectedWord.ExampleCorectly;
                      SelectedWord.HidedExample = null;
                  });
            }
        }

        public RelayCommand ShowExampleCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      SelectedWord.SourcePictureExample = null;
                      SelectedWord.Example = SelectedWord.ExampleCorectly;
                  });
            }
        }

        public RelayCommand GoBackCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      View.Menu.Frame.NavigationService.GoBack();
                      if (updater != null)
                          updater.Update();
                  });
            }
        }

        public RelayCommand PreviouslyCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      if (IndexSelectedWord - 1 != 0)
                      {
                          IndexSelectedWord--;
                          SelectedWord = Words[IndexSelectedWord - 1];
                      }
                  });
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      if (SelectedWord != null)
                      {
                          View.Menu.Frame.Navigate(new Edit(SelectedWord.Id.ToString(), this));
                      }
                  });
            }
        }

        public RelayCommand UnknownWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      KnowCommand(0);
                  });
            }
        }

        public RelayCommand NotMemorizedWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      KnowCommand(33);
                  });
            }
        }

        public RelayCommand MemorizedWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      KnowCommand(66);
                  });
            }
        }

        public RelayCommand LearnedWordsCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      KnowCommand(100);
                  });
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
        private void KnowCommand(int percent)
        {
            if (SelectedWord != null)
            {
                Resource.getInstance().WordService.SetKnow(SelectedWord.Id.ToString(), percent);
                int index = Words.IndexOf(SelectedWord);
                var word = new WordViewModel(Resource.getInstance().WordService.GetWord(SelectedWord.Id.ToString()), index);
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
                    (new Warning(Application.Current.Resources["EndOfList"].ToString())).ShowDialog();
                }
            }
        }

        public void Update()
        {
            int index = Words.IndexOf(SelectedWord);
            var word = new WordViewModel(Resource.getInstance().WordService.GetWord(SelectedWord.Id.ToString()), index);
            Words.RemoveAt(index);
            Words.Insert(index, word);
            SelectedWord = null;
            SelectedWord = Words[index];
            TotalCount = Words.Count;
        }
    }
}
