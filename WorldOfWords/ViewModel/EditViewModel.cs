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
            if(id != null)
            {
                var word = Resource.getInstance().WordService.GetWord(id);  
                if(word != null)
                {
                    NewWord = new WordViewModel(word.Priority)
                    {
                        Id = word.Id,
                        Name = word.Name,
                        Translate = word.Translate,
                        Example = word.Example,
                        Level = word.Level,
                        LastUpdateDate = word.LastUpdate,
                        Picture = word.Picture,
                        SourcePicture = word.Picture != null ? Resource.getInstance().WordService.GetSourceImage(word.Picture) : null,
                    };

                    if(NewWord.Picture == null)
                    {
                        NewWord.SourcePicture = Resource.getInstance().SourceNoImage;
                    }
                }
            }
            else
            {
                NewWord = new WordViewModel(0)
                {
                    SourcePicture = Resource.getInstance().SourceNoImage,
                };
            }
            this.updater = updater;
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
                      NewWord.Picture = Resource.getInstance().WordService.FindImage() ?? NewWord.Picture;
                      if (NewWord.Picture != null)
                      {
                          NewWord.SourcePicture = Resource.getInstance().WordService.GetSourceImage(NewWord.Picture);
                      }
                      else
                      {
                          NewWord.SourcePicture = Resource.getInstance().SourceNoImage;
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
                      DeletePicture();
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
                      if (!IsEnteredName())
                      {
                          EditPage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteWord"].ToString());
                          return;
                      }
                      if (!IsEnteredTranslate())
                      {
                          EditPage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteTranslate"].ToString());
                          return;
                      }

                      Resource.getInstance().WordService.Edit(CreateWordArgs());
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


        public bool IsEnteredName()
        {
            return string.IsNullOrEmpty(NewWord.Name);
        }

        public bool IsEnteredTranslate()
        {
            return string.IsNullOrEmpty(NewWord.Translate);
        }

        public WordArgs CreateWordArgs()
        {
            return new WordArgs()
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
        }

        public void DeletePicture()
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
        }
    }
}
