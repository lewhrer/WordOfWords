using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class CreateViewModel : INotifyPropertyChanged
    {
        IUpdater updater;
        Create CreatePage { get; set; }
        public WordViewModel NewWord { get; set; }

        public CreateViewModel(IUpdater updater, Create createPage)
        {
            NewWord = new WordViewModel()
            {
                SourcePicture = Resource.getInstance().SourceNoImage,
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
                      if(!IsEnteredName())
                      {
                          CreatePage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteWord"].ToString());
                          return;
                      }
                      if(!IsEnteredTranslate())
                      {
                          CreatePage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteTranslate"].ToString());
                          return;
                      }

                      Resource.getInstance().WordService.Create(CreateWordArgs());
                      ResetNewWord();

                      CreatePage.ActionResult(new SolidColorBrush(Colors.Green), Application.Current.Resources["WordCreatedSucsassfull"].ToString());
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

        public void ResetNewWord()
        {
            NewWord.SourcePicture = Resource.getInstance().SourceNoImage;
            NewWord.Example = null;
            NewWord.Name = null;
            NewWord.Translate = null;
            NewWord.WordPriority = NewWord.Priorities[0];
        }

        public WordArgs CreateWordArgs()
        {
            return new WordArgs()
            {
                Example = NewWord.Example,
                Name = NewWord.Name,
                Picture = NewWord.Picture,
                LastUpdate = DateTime.Now,
                Translate = NewWord.Translate,
                Level = 0,
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
