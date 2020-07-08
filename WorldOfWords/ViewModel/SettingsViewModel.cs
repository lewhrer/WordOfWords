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
using WorldOfWords.Model;

namespace WorldOfWords.ViewModel
{
    public class SettingsViewModel
    {
        public SettingsViewModel()
        {
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      Serialisation();
                  }));
            }
        }

        private RelayCommand desaveCommand;
        public RelayCommand DesaveCommand
        {
            get
            {
                return desaveCommand ??
                  (desaveCommand = new RelayCommand(obj =>
                  {
                      Deserialisation();
                  }));
            }
        }

        private RelayCommand deleteAllCommand;
        public RelayCommand DeleteAllCommand
        {
            get
            {
                return deleteAllCommand ??
                  (deleteAllCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().WordService.DeleteEverything();
                  }));
            }
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

        private void Serialisation()
        {
            var words = Resource.getInstance().WordService.GetAllWords().ToArray();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Word[]));

            using (FileStream fs = new FileStream("words.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, words);
            }

            Console.ReadLine();
        }

        private void Deserialisation()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Word[]));

            using (FileStream fs = new FileStream("words.json", FileMode.OpenOrCreate))
            {
                Word[] words = (Word[])jsonFormatter.ReadObject(fs);

                Resource.getInstance().WordService.DeleteEverything();
                Resource.getInstance().WordService.AddRange(words);
            }
        }
    }
}
