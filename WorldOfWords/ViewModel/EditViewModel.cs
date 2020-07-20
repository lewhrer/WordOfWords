using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class EditViewModel : INotifyPropertyChanged
    {
        IUpdater updater;
        Edit EditPage { get; set; }
        public WordViewModel NewWord { get; set; }

        public EditViewModel(string id, IUpdater updater, Edit editPage)
        {
            var word = Resource.getInstance().WordService.GetWord(id);
            this.updater = updater;
            NewWord = new WordViewModel(word.Priority)
            {
                Id = word.Id,
                Name = word.Name,
                Translate = word.Translate,
                Example = word.Example,
                Level = word.Level,
                LastUpdateDate = word.LastUpdate,
                Picture = word.Picture,
                SourcePicture = Resource.getInstance().WordService.GetSourceImage(word.Picture),
            };

            if(NewWord.Picture == null)
            {
                NewWord.SourcePicture = Resource.getInstance().SourceNoImage;
            }
            EditPage = editPage;
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      NewWord.Picture = Resource.getInstance().WordService.FindImage();
                      NewWord.SourcePicture = Resource.getInstance().WordService.GetSourceImage(NewWord.Picture);
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
                          NewWord.SourcePicture = Resource.getInstance().SourceNoImage;
                      }
                      catch (Exception e)
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
                      if (string.IsNullOrEmpty(NewWord.Name))
                      {
                          EditPage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteWord"].ToString());
                          return;
                      }
                      if (string.IsNullOrEmpty(NewWord.Translate))
                      {
                          EditPage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteTranslate"].ToString());
                          return;
                      }

                      var args = new WordArgs()
                      {
                          Id = NewWord.Id.ToString(),
                          Example = NewWord.Example,
                          Name = NewWord.Name,
                          Picture = NewWord.Picture,
                          LastUpdate = NewWord.LastUpdateDate,
                          Translate = NewWord.Translate,
                          Level = NewWord.Level,
                          Priority = int.Parse(NewWord.WordPriority.Content.ToString()),
                      };

                      Resource.getInstance().WordService.Edit(args);
                      updater.Update();
                      View.Menu.Frame.NavigationService.GoBack();
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
                      updater.Update();
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
