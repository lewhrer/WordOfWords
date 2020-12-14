using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class ListOfThemesViewModel : BaseViewModel
    {
        Theme selectedTheme;
        int totalCount;

        public ObservableCollection<Theme> Themes { get; set; }

        public ListOfThemesViewModel()
        {
            var themes = Resource.getInstance().ThemeService.GetThemes();
            Themes = new ObservableCollection<Theme>(themes.Select(x => new Theme() { Name = x.Name, Id = x.Id, }));
            TotalCount = Themes.Count;
            ListOfThemes.Frame.Navigate(new CreateTheme());
        }

        public RelayCommand MoreCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (selectedTheme != null)
                    {
                        View.Menu.Frame.Navigate(new ListOfWords(selectedTheme));
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["FirstSelectTheme"].ToString())).ShowDialog();
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
                    if (selectedTheme != null)
                    {
                        ListOfThemes.Frame.Navigate(new EditTheme(selectedTheme));
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["FirstSelectTheme"].ToString())).ShowDialog();
                    }
                });
            }
        }

        public RelayCommand TrainCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (selectedTheme != null)
                    {

                        var wrd = Resource.getInstance().ThemeService.GetTheme(selectedTheme.Id.ToString()).Words.ToList();
                        if (wrd.Count != 0)
                        {
                            (new MenuViewModel()).TrainCommand(wrd,
                            Application.Current.Resources["AllWords"].ToString(),
                                "dd", "d");
                        }
                        else
                        {
                            (new Warning(Application.Current.Resources["EndOfList"].ToString())).ShowDialog();

                        }
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["FirstSelectTheme"].ToString())).ShowDialog();
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
                    if (selectedTheme != null)
                    {
                        DeleteCommandAsync();
                        Themes.Remove(selectedTheme);
                    }
                    else
                    {
                        (new Warning(Application.Current.Resources["FirstSelectTheme"].ToString())).ShowDialog();
                    }
                });
            }
        }
        async void DeleteCommandAsync()
        {
            await Resource.getInstance().ThemeService.Delete(selectedTheme.Id.ToString());
        }

        public Theme SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                selectedTheme = value;
                OnPropertyChanged("SelectedTheme");
            }
        }
        public int TotalCount
        {
            get { return totalCount; }
            set
            {
                totalCount = value;
                OnPropertyChanged(nameof(TotalCount));
            }
        }
    }
}
