using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldOfWords.Infrastructure.Services;

namespace WorldOfWords.ViewModel
{
    public class SettingsViewModel
    {
        Frame _menuFrame;
        public IWordService _wordService;

        public SettingsViewModel(Frame menuFrame, IWordService wordService)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
        }

        private RelayCommand doubleCommand;
        public RelayCommand DoubleCommand
        {
            get
            {
                return doubleCommand ??
                    (doubleCommand = new RelayCommand(obj =>
                    {
                        ResourceDictionary myResourceDictionary = new ResourceDictionary();
                        myResourceDictionary.Source = new Uri(obj.ToString());
                        Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
                    }));
            }
        }
    }
}
