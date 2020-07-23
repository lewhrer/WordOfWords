using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows;
using WorldOfWords.Infrastructure;
using WorldOfWords.Model;

namespace WorldOfWords
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
            DeserialisationSettings();
            BeginInitial();
        }

        private void DeserialisationSettings()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Resource));

            try
            {
                Directory.CreateDirectory("Data");
                using (FileStream fs = new FileStream("Data/Settings.json", FileMode.OpenOrCreate))
                {
                    var resource = (Resource)jsonFormatter.ReadObject(fs);
                    if (resource != null)
                    {
                        Resource.getInstance().ThemePath = resource.ThemePath;
                        Resource.getInstance().Theme = resource.Theme;
                        Resource.getInstance().PathNoImage = resource.PathNoImage;
                        Resource.getInstance().Level = resource.Level;
                        Resource.getInstance().TrainDate = resource.TrainDate;
                        Resource.getInstance().Language = resource.Language;
                        Resource.getInstance().LanguagePath = resource.LanguagePath;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }


        private void BeginInitial()
        {
            ResourceDictionary myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source = new Uri(Resource.getInstance().ThemePath);
            Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
            if (Resource.getInstance().ThemePath.Contains("Dark"))
            {
                ResourceDictionary theme = new ResourceDictionary();
                theme.Source = new Uri($"pack://application:,,,/WorldOfWords;component/Themes/DarkTheme.xaml");
                Application.Current.Resources.MergedDictionaries.Add(theme);
            }
            else
            {
                ResourceDictionary theme = new ResourceDictionary();
                theme.Source = new Uri($"pack://application:,,,/WorldOfWords;component/Themes/LightTheme.xaml");
                Application.Current.Resources.MergedDictionaries.Add(theme);
            }
            ResourceDictionary language = new ResourceDictionary();
            language.Source = new Uri(Resource.getInstance().LanguagePath);
            Application.Current.Resources.MergedDictionaries.Add(language);
        }
    }
}
