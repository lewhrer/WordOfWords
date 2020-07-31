using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class WordInfoViewModel : INotifyPropertyChanged, IUpdater
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
                          Resource.getInstance().WordService.Delete(SelectedWord.Id.ToString());
                          Words.RemoveAt(Words.IndexOf(SelectedWord));
                          TotalCount--;
                          (new Warning(Application.Current.Resources["WordWasDeletedSuccessfully"].ToString())).ShowDialog();
                          if(Words.Count == 0)
                          {
                              View.Menu.Frame.NavigationService.GoBack();
                              if (updater != null)
                                  updater.Update();
                          }
                          else if(Words.Count == IndexSelectedWord - 1)
                          {
                              IndexSelectedWord--;
                              SelectedWord = Words[IndexSelectedWord - 1];
                          }
                          else
                          {
                              SelectedWord = Words[IndexSelectedWord - 1];
                          }
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
                          SelectedWord = Words[IndexSelectedWord++];
                      }
                      else
                      {
                          (new Warning(Application.Current.Resources["EndOfList"].ToString())).ShowDialog();
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

        private RelayCommand mouseDownOnPictureCommand;
        public RelayCommand MouseDownOnPictureCommand
        {
            get
            {
                return mouseDownOnPictureCommand ??
                  (mouseDownOnPictureCommand = new RelayCommand(obj =>
                  {
                        if(SelectedWord.SourcePicture != null)
                        {
                            View.Menu.Frame.Navigate(new PhotoViewer(SelectedWord.SourcePicture));
                        }
                  }));
            }
        }

        private RelayCommand mouseDownOnHidedPictureCommand;
        public RelayCommand MouseDownOnHidedPictureCommand
        {
            get
            {
                return mouseDownOnHidedPictureCommand ??
                  (mouseDownOnHidedPictureCommand = new RelayCommand(obj =>
                  {
                      SelectedWord.HidedPicture = null;
                      SelectedWord.SourcePicture = SelectedWord.SourcePictureCorectly;
                  }));
            }
        }

        private RelayCommand mouseDownOnTranslateCommand;
        public RelayCommand MouseDownOnTranslateCommand
        {
            get
            {
                return mouseDownOnTranslateCommand ??
                  (mouseDownOnTranslateCommand = new RelayCommand(obj =>
                  {
                      SelectedWord.HidedTranslate = SelectedWord.Translate;
                  }));
            }
        }

        private RelayCommand mouseDownOnExampleCommand;
        public RelayCommand MouseDownOnExampleCommand
        {
            get
            {
                return mouseDownOnExampleCommand ??
                  (mouseDownOnExampleCommand = new RelayCommand(obj =>
                  {
                      SelectedWord.Example = SelectedWord.ExampleCorectly;
                      SelectedWord.HidedExample = null;
                  }));
            }
        }

        private RelayCommand showExampleCommand;
        public RelayCommand ShowExampleCommand
        {
            get
            {
                return showExampleCommand ??
                  (showExampleCommand = new RelayCommand(obj =>
                  {
                      SelectedWord.SourcePictureExample = null;
                      SelectedWord.Example = SelectedWord.ExampleCorectly;
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
                      View.Menu.Frame.NavigationService.GoBack();
                      if(updater != null)
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
                          IndexSelectedWord--;
                          SelectedWord = Words[IndexSelectedWord - 1];
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
                          View.Menu.Frame.Navigate(new Edit(SelectedWord.Id.ToString(), this));
                      }
                  }));
            }
        }

        private RelayCommand unknownWordsCommand;
        public RelayCommand UnknownWordsCommand
        {
            get
            {
                return unknownWordsCommand ??
                  (unknownWordsCommand = new RelayCommand(obj =>
                  {
                      KnowCommand(0);
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
                      KnowCommand(33);
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
                      KnowCommand(66);
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
                      KnowCommand(100);
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
