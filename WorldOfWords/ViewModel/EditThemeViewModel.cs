using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class EditThemeViewModel : BaseViewModel
    {
        private string themeName;

        private Theme themeModel;

        public EditThemeViewModel(Theme theme)
        {
            Theme = theme;
            themeName = theme.Name;
        }

        public Theme Theme
        {
            get { return themeModel; }
            set
            {
                themeModel = value;
                OnPropertyChanged(nameof(Theme));
            }
        }
        public string ThemeName
        {
            get { return themeName; }
            set
            {
                themeName = value;
                OnPropertyChanged(nameof(themeName));
            }
        }
        public RelayCommand EditCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    var args = new ThemeArgs() { Id = Theme.Id.ToString(), Name = Theme.Name, Words = Theme.Words, };
                    Resource.getInstance().ThemeService.Edit(args);
                    ListOfThemes.Frame.Navigate(new CreateTheme());
                });
            }
        }

        public RelayCommand BackCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    ListOfThemes.Frame.Navigate(new CreateTheme());
                });
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (String.IsNullOrEmpty(ThemeName))
                    {
                        (new Warning(Application.Current.Resources["DontWroteWord"].ToString())).ShowDialog();
                        return;
                    }
                    else
                    {
                        Resource.getInstance().ThemeService.Edit(new ThemeArgs() { Id = themeModel.Id.ToString(), Name = themeName });
                        ListOfThemes.Frame.Navigate(new CreateTheme());
                        Menu.Frame.Navigate(new ListOfThemes());

                    }
                });
            }
        }
    }
}
