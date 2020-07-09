using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;

namespace WorldOfWords.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public Level Level { get; set; }
        public TrainDate TrainDate { get; set; }

        public SettingsViewModel()
        {
            Level = Resource.getInstance().Level;
            TrainDate = Resource.getInstance().TrainDate;
        }

        private RelayCommand makeReserveCommand;
        public RelayCommand MakeReserveCommand
        {
            get
            {
                return makeReserveCommand ??
                  (makeReserveCommand = new RelayCommand(obj =>
                  {
                      Serialisation();
                  }));
            }
        }

        private RelayCommand uploadReserveCommand;
        public RelayCommand UploadReserveCommand
        {
            get
            {
                return uploadReserveCommand ??
                  (uploadReserveCommand = new RelayCommand(obj =>
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

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().Level = Level;
                      Resource.getInstance().TrainDate = TrainDate;
                      DeleteFile("Settings.json");

                      DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Resource));

                      using (FileStream fs = new FileStream("Settings.json", FileMode.OpenOrCreate))
                      {
                          jsonFormatter.WriteObject(fs, Resource.getInstance());
                      }
                      MessageBox.Show("Дані збережено успішно!");
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
                        Resource.getInstance().ThemePath = obj.ToString();
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
