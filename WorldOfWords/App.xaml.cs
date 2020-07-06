using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows;

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
                using (FileStream fs = new FileStream("Settings.json", FileMode.Open))
                {
                    var resource = (Resource)jsonFormatter.ReadObject(fs);
                    if (resource != null)
                    {
                        Resource.getInstance().ThemePath = resource.ThemePath;
                        Resource.getInstance().PathNoImage = resource.PathNoImage;

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
            //else
            //{
            //    myResourceDictionary.Source = new Uri($"pack://application:,,,/WorldOfWords;component/Themes/LightTheme.xaml");
            //}
        }
    }
}
