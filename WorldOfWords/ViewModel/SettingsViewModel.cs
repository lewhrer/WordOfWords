using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

        private RelayCommand styleCommand;
        public RelayCommand StyleCommand
        {
            get
            {
                return styleCommand ??
                    (styleCommand = new RelayCommand(obj =>
                    {
                        DeleteFile("Settings.json");
             
                        Resource.getInstance().ThemePath = obj.ToString();
                        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Resource));

                        using (FileStream fs = new FileStream("Settings.json", FileMode.OpenOrCreate))
                        {
                            jsonFormatter.WriteObject(fs, Resource.getInstance());
                        }
                        ResourceDictionary myResourceDictionary = new ResourceDictionary();
                        myResourceDictionary.Source = new Uri(obj.ToString());
                        Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
                        ResourceDictionary theme = new ResourceDictionary();
                        if (obj.ToString().Contains("Dark"))
                        {
                            theme.Source = new Uri($"pack://application:,,,/WorldOfWords;component/Themes/DarkTheme.xaml");
                        }
                        else
                        {
                            theme.Source = new Uri($"pack://application:,,,/WorldOfWords;component/Themes/LightTheme.xaml");
                        }
                        Application.Current.Resources.MergedDictionaries.Add(theme);
                    }));
            }
        }

        void DeleteFile(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }
    }
}
