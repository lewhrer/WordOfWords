using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Models;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class CreateThemeViewModel : BaseViewModel
    {
        private string name;

        public CreateThemeViewModel()
        {

        }
        public CreateThemeViewModel(string tm)
        { 

        }
        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    SaveCommandAsynk();
                });
            }
        }
        async void SaveCommandAsynk()
        {
            var args = new ThemeArgs() { Name = name, };
            await Resource.getInstance().ThemeService.Create(args);
            Menu.Frame.Navigate(new ListOfThemes());

        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
