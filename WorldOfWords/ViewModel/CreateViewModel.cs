using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class CreateViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        IWordService _wordService;
        IUpdater updater;
        Create CreatePage { get; set; }
        public WordViewModel NewWord { get; set; }

        public CreateViewModel(Frame menuFrame, IWordService wordService, IUpdater updater, Create createPage)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            NewWord = new WordViewModel()
            {
                SourcePicture = Resource.sourceNoImage,
            };
            this.updater = updater;
            CreatePage = createPage;
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      NewWord.Picture = _wordService.FindImage();
                      NewWord.SourcePicture = _wordService.GetSourceImage(NewWord.Picture);
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
                      NewWord.Picture = null;
                      try
                      {
                          NewWord.SourcePicture = Resource.sourceNoImage;
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      if(string.IsNullOrEmpty(NewWord.Name))
                      {
                          CreatePage.ActionResult(new SolidColorBrush(Colors.Red), "Не введено назву слова!");
                          return;
                      }
                      if(string.IsNullOrEmpty(NewWord.Translate))
                      {
                          CreatePage.ActionResult(new SolidColorBrush(Colors.Red), "Не введено переклад слова!");
                          return;
                      }

                      var args = new WordArgs()
                      {
                          Example = NewWord.Example,
                          Name = NewWord.Name,
                          Picture = NewWord.Picture,
                          LastUpdate = new DateTime(2000, 1, 1),
                          Translate = NewWord.Translate,
                          Level = 0,
                          Priority = int.Parse(NewWord.WordPriority.Content.ToString()),
                      };

                      _wordService.Create(args);
                      var create = new Create(_menuFrame, _wordService, updater);
                      _menuFrame.Navigate(create);
                      create.ActionResult(new SolidColorBrush(Colors.Green), "Слово створене успішно!");
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
                      updater.Update();
                      _menuFrame.GoBack();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
