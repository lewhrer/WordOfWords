using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using WorldOfWords.Model;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private ComboBoxItem language;
        private readonly string settingsPath = "Data/Settings.json";
        private readonly string wordsPath = "Data/Words.json";
        public Level Level { get; set; }
        public TrainDate TrainDate { get; set; }
        public ObservableCollection<ComboBoxItem> Languages { get; set; }


        public SettingsViewModel()
        {
            Level = Resource.getInstance().Level;
            TrainDate = Resource.getInstance().TrainDate;
            Languages = new ObservableCollection<ComboBoxItem>()
            {
                new ComboBoxItem(){ Content = "Українська", Tag="pack://application:,,,/WorldOfWords;component/Resources/LanguageUkrainian.xaml"},
                new ComboBoxItem(){ Content = "English", Tag="pack://application:,,,/WorldOfWords;component/Resources/LanguageEnglish.xaml"},
            };
            Language = Languages[Languages.IndexOf(Languages.First(x => x.Content.ToString() == Resource.getInstance().Language))];
        }

        private RelayCommand makeReserveCommand;
        public RelayCommand MakeReserveCommand
        {
            get
            {
                return makeReserveCommand ??
                  (makeReserveCommand = new RelayCommand(obj =>
                  {
                      Settings.Details.Navigate(new Details(Application.Current.Resources["ReserveMessage"].ToString(), Serialisation));
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
                      Settings.Details.Navigate(new Details(Application.Current.Resources["DereserveMessage"].ToString(), Deserialisation));
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
                      Settings.Details.Navigate(new Details(Application.Current.Resources["AllDeleteMessage"].ToString(), Resource.getInstance().WordService.DeleteEverything));
                  }));
            }
        }

        private RelayCommand applyCommand;
        public RelayCommand ApplyCommand
        {
            get
            {
                return applyCommand ??
                  (applyCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().Level = Level;
                      Resource.getInstance().TrainDate = TrainDate;
                      DeleteFile(settingsPath);

                      DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Resource));

                      using (FileStream fs = new FileStream(settingsPath, FileMode.OpenOrCreate))
                      {
                          jsonFormatter.WriteObject(fs, Resource.getInstance());
                      }
                      (new Warning(Application.Current.Resources["DataWasSavedSuccessfully"].ToString())).ShowDialog(); 
                  }));
            }
        }

        private RelayCommand applyLanguageCommand;
        public RelayCommand ApplyLanguageCommand
        {
            get
            {
                return applyLanguageCommand ??
                  (applyLanguageCommand = new RelayCommand(obj =>
                  {
                      ResourceDictionary myResourceDictionary = new ResourceDictionary();
                      myResourceDictionary.Source = new Uri(Language.Tag.ToString());
                      Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
                      Resource.getInstance().Language = Language.Content.ToString();
                      Resource.getInstance().LanguagePath = Language.Tag.ToString();

                      DeleteFile(settingsPath);

                      DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Resource));

                      using (FileStream fs = new FileStream(settingsPath, FileMode.OpenOrCreate))
                      {
                          jsonFormatter.WriteObject(fs, Resource.getInstance());
                      }
                      MainWindow.Frame.Navigate(new View.Menu(0));
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
                            Resource.getInstance().Theme = "pack://application:,,,/WorldOfWords;component/Themes/DarkTheme.xaml";
                            theme.Source = new Uri(Resource.getInstance().Theme);
                        }
                        else
                        {
                            Resource.getInstance().Theme = "pack://application:,,,/WorldOfWords;component/Themes/LightTheme.xaml";
                            theme.Source = new Uri(Resource.getInstance().Theme);
                        }
                        Application.Current.Resources.MergedDictionaries.Add(theme);

                        DeleteFile(settingsPath);

                        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Resource));

                        using (FileStream fs = new FileStream(settingsPath, FileMode.OpenOrCreate))
                        {
                            jsonFormatter.WriteObject(fs, Resource.getInstance());
                        }
                    }));
            }
        }

        public ComboBoxItem Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged("Language");
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
            DeleteFile(wordsPath);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Word[]));

            using (FileStream fs = new FileStream(wordsPath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, words);
            }
        }

        private void Deserialisation()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Word[]));

            using (FileStream fs = new FileStream(wordsPath, FileMode.OpenOrCreate))
            {
                try
                {
                    Word[] words = (Word[])jsonFormatter.ReadObject(fs);
                    Resource.getInstance().WordService.AddRange(words);
                }
                catch(Exception)
                {
                    (new Warning(Application.Current.Resources["ReserveIsEmpty"].ToString())).ShowDialog();
                }
            }
        }
    }
}
