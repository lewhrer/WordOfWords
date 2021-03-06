﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class ListOfWordsViewModel : BaseViewModel, IUpdater
    {
        WordViewModel selectedWord;
        Theme theme;
        string nameMethod;
        string namePage;
        string nameTrainPage;
        int totalCount;

        public ObservableCollection<WordViewModel> Words { get; set; }

        public ListOfWordsViewModel(string nameMethod, string namePage, string nameTrainPage)
        {
            this.nameMethod = nameMethod;
            this.nameTrainPage = nameTrainPage;
            NamePage = namePage;
            Words = new ObservableCollection<WordViewModel>(GetWords(nameMethod));
            TotalCount = Words.Count;
        }
        public ListOfWordsViewModel(Theme the)
        {
            theme = the;

            var th = Resource.getInstance().ThemeService.GetTheme(the.Id.ToString()).Words;
            if (th != null)
            {
                var t = th.Select(x => new WordViewModel() { Id = x.Id, Name = x.Name, Picture = x.Picture, Translate = x.Translate });
                Words = new ObservableCollection<WordViewModel>(t);
            }
        }
        public RelayCommand MoreCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (selectedWord != null)
                    {
                        int index = Words.IndexOf(selectedWord);
                        View.Menu.Frame.Navigate(new WordInfo(Words.ToList(), nameTrainPage, this, index));
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["FirstSelectWord"].ToString())).ShowDialog();
                    }
                });
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (selectedWord != null)
                    {
                        Resource.getInstance().WordService.Delete(SelectedWord.Id.ToString());
                        DeleteWord();
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["FirstSelectWord"].ToString())).ShowDialog();
                    }
                });
            }
        }


        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    View.Menu.Frame.Navigate(new Create(theme));
                });
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (selectedWord != null)
                    {
                        View.Menu.Frame.Navigate(new Edit(SelectedWord.Id.ToString(), this));
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["FirstSelectWord"].ToString())).ShowDialog();
                    }
                });
            }
        }

        #region KnowCommand

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

        public void DeleteWord()
        {
            Words.RemoveAt(Words.IndexOf(SelectedWord));
            TotalCount--;
        }

        public void KnowCommand(int percent)
        {
            if (selectedWord != null)
            {
                Resource.getInstance().WordService.SetKnow(SelectedWord.Id.ToString(), percent);
                int index = Words.IndexOf(SelectedWord);
                var word = new WordViewModel(Resource.getInstance().WordService.GetWord(SelectedWord.Id.ToString()), index);
                Words.RemoveAt(index);
                Words.Insert(index, word);
                SelectedWord = word;
            }
            else
            {
                (new Warning(Application.Current.Resources["FirstSelectWord"].ToString())).ShowDialog();
            }
        }

        public List<WordViewModel> GetWords(string nameOfMethod)
        {
            switch (nameMethod)
            {
                case "All":
                    {
                        var words = Resource.getInstance().WordService.GetAllWords();
                        return words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList();
                    }
                case "0":
                    {
                        var words = Resource.getInstance().WordService.GetWords(0, Resource.getInstance().Level.First);
                        return words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList();
                    }
                case "33":
                    {
                        var words = Resource.getInstance().WordService.GetWords(Resource.getInstance().Level.First, Resource.getInstance().Level.Second);
                        return words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList();
                    }
                case "66":
                    {
                        var words = Resource.getInstance().WordService.GetWords(Resource.getInstance().Level.Second, Resource.getInstance().Level.Third);
                        return words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList();
                    }
                case "100":
                    {
                        var words = Resource.getInstance().WordService.GetWords(Resource.getInstance().Level.Third, 100);
                        return words.Select(x => new WordViewModel(x, words.IndexOf(x))).ToList();
                    }
                default: return new List<WordViewModel>();
            }
        }

        public void Update()
        {
            Words.Clear();
            List<WordViewModel> newWords = GetWords(nameMethod);
            TotalCount = newWords.Count;

            foreach (var item in newWords)
            {
                Words.Add(item);
            }
            if (theme == null)
            {
                View.Menu.Frame.Navigate(new ListOfThemes());

            }
            else
            {
                View.Menu.Frame.Navigate(new ListOfWords(theme));
            }
            SelectedWord = null;

        }
    }
}
