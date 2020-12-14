using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class CreateViewModel : BaseViewModel
    {
        IUpdater updater;
        Create CreatePage { get; set; }
        public WordViewModel NewWord { get; set; }
        Theme theme;
        public CreateViewModel(IUpdater updater, Create createPage)
        {
            NewWord = new WordViewModel()
            {
                SourcePicture = Resource.getInstance().SourceNoImage,
            };
            this.updater = updater;
            CreatePage = createPage;
        }
        public CreateViewModel(Theme the, IUpdater updater, Create createPage)
        {
            theme = the;
            NewWord = new WordViewModel()
            {
                SourcePicture = Resource.getInstance().SourceNoImage,
            };
            this.updater = updater;
            CreatePage = createPage;
        }


        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(obj =>
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
                 });
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      DeletePicture();
                  });
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      if (IsEnteredName())
                      {
                          CreatePage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteWord"].ToString());
                          return;
                      }
                      if (IsEnteredTranslate())
                      {
                          CreatePage.ActionResult(new SolidColorBrush(Colors.Red), Application.Current.Resources["DontWroteTranslate"].ToString());
                          return;
                      }

                      Resource.getInstance().WordService.Create(CreateWordArgs());
                      ResetNewWord();

                      CreatePage.ActionResult(new SolidColorBrush(Colors.Green), Application.Current.Resources["WordCreatedSucsassfull"].ToString());
                  });
            }
        }
        public RelayCommand GoBackCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    View.Menu.Frame.Navigate(new ListOfWords(theme));
                });
            }
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
            NewWord.Picture = null;
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
                ThemeId = theme.Id,
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
