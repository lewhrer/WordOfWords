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
    public class ListOfWordsViewModel : INotifyPropertyChanged, IUpdater
    {
        WordViewModel selectedWord;
        string nameMethod;
        string namePage;
        int totalCount;

        public ObservableCollection<WordViewModel> Words { get; set; }

        public ListOfWordsViewModel(string nameMethod, string namePage)
        {
            this.nameMethod = nameMethod;
            NamePage = namePage;
            switch (nameMethod)
            {
                case "All": Words = new ObservableCollection<WordViewModel>(Resource.getInstance().WordService.GetAllWords().Select(x => new WordViewModel(x))); break;
                case "0": Words = new ObservableCollection<WordViewModel>(Resource.getInstance().WordService.GetNotStudiedWords().Select(x => new WordViewModel(x))); break;
                case "25": Words = new ObservableCollection<WordViewModel>(Resource.getInstance().WordService.GetAlmostAcquaintedWords().Select(x => new WordViewModel(x))); break;
                case "50": Words = new ObservableCollection<WordViewModel>(Resource.getInstance().WordService.GetAcquaintedWords().Select(x => new WordViewModel(x))); break;
                case "75": Words = new ObservableCollection<WordViewModel>(Resource.getInstance().WordService.GetAlmostStudiedWords().Select(x => new WordViewModel(x))); break;
                case "100": Words = new ObservableCollection<WordViewModel>(Resource.getInstance().WordService.GetStudiedWords().Select(x => new WordViewModel(x))); break;
                default: MessageBox.Show("bad name of method"); break;
            }
            TotalCount = Words.Count;
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
                          Resource.getInstance().MenuFrame.Navigate(new WordInfo(Words.ToList(), this, $"{nameMethod} words", index));
                      }
                      else
                      {
                          MessageBox.Show("Спочатку виберіть слово.");
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
                          Resource.getInstance().WordService.Delete(SelectedWord.Id.ToString());
                          Words.RemoveAt(Words.IndexOf(selectedWord));
                          TotalCount--;
                      }
                      else
                      {
                          MessageBox.Show("Спочатку виберіть слово.");
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
                          Resource.getInstance().MenuFrame.Navigate(new Edit(SelectedWord.Id.ToString(), this));
                      }
                      else
                      {
                          MessageBox.Show("Спочатку виберіть слово.");
                      }
                  }));
            }
        }

        #region KnowCommand

        private RelayCommand know0Command;
        public RelayCommand Know0Command
        {
            get
            {
                return know0Command ??
                  (know0Command = new RelayCommand(obj =>
                  {
                      KnowCommand(0);
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
                      KnowCommand(25);
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
                      KnowCommand(50);
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
                      KnowCommand(75);
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
                      KnowCommand(100);
                  }));
            }
        }

        #endregion

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
            if (selectedWord != null)
            {
                Resource.getInstance().WordService.SetKnow(SelectedWord.Id.ToString(), percent);
                var word = new WordViewModel(Resource.getInstance().WordService.GetWord(SelectedWord.Id.ToString()));
                int index = Words.IndexOf(SelectedWord);
                Words.RemoveAt(index);
                Words.Insert(index, word);
                SelectedWord = word;
            }
            else
            {
                MessageBox.Show("Спочатку виберіть слово.");
            }
        }

        public void Update()
        {
            Words.Clear();
            List<WordViewModel> words = new List<WordViewModel>();
            switch (nameMethod)
            {
                case "All": words = Resource.getInstance().WordService.GetAllWords().Select(x => new WordViewModel(x)).ToList(); break;
                case "0": words = Resource.getInstance().WordService.GetNotStudiedWords().Select(x => new WordViewModel(x)).ToList(); break;
                case "25": words = Resource.getInstance().WordService.GetAlmostAcquaintedWords().Select(x => new WordViewModel(x)).ToList(); break;
                case "50": words = Resource.getInstance().WordService.GetAcquaintedWords().Select(x => new WordViewModel(x)).ToList(); break;
                case "75": words = Resource.getInstance().WordService.GetAlmostStudiedWords().Select(x => new WordViewModel(x)).ToList(); break;
                case "100": words = Resource.getInstance().WordService.GetStudiedWords().Select(x => new WordViewModel(x)).ToList(); break;
                default: MessageBox.Show("bad name of method"); break;
            }

            TotalCount = words.Count;
                
            foreach (var item in words)
            {
                Words.Add(item);
            }

            try
            {
                var word = new WordViewModel(Resource.getInstance().WordService.GetWord(SelectedWord.Id.ToString()));
                int index = Words.IndexOf(SelectedWord);
                Words.RemoveAt(index);
                Words.Insert(index, word);
                SelectedWord = word;
            }
            catch (Exception) { }
        }
    }
}
