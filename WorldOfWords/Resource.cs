using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;
using WorldOfWords.Infrastructure.Services;
using System.Windows.Controls;
using WorldOfWords.Infrastructure;

namespace WorldOfWords
{
    [DataContract]
    public class Resource
    {
        private static Resource instance;
        [DataMember]
        private string pathNoImage;
        [DataMember]
        private string themePath;
        [DataMember]
        private string theme;
        [DataMember]
        private Level level;
        [DataMember]
        private TrainDate trainDate;
        [DataMember]
        private string languagePath;
        [DataMember]
        private string language;

        public Resource()
        {
            PathNoImage = "pack://application:,,,/WorldOfWords;component/Resources/Image.png";
            Theme = "pack://application:,,,/WorldOfWords;component/Themes/DarkTheme.xaml";
            ThemePath = "pack://application:,,,/MahApps.Metro;component/Styles/Themes/Dark.Blue.xaml";
            LanguagePath = "pack://application:,,,/WorldOfWords;component/Resources/LanguageUkrainian.xaml";
            Language = "Українська";
            WordService = new WordService(new WorldOfWordsDbContext());
            Level = new Level();
            TrainDate = new TrainDate();
        }

        public static Resource getInstance()
        {
            if (instance == null)
                instance = new Resource();
            return instance;
        }

        public BitmapImage SourceNoImage { get { return new BitmapImage(new Uri(pathNoImage)); } }

        public IWordService WordService { get; set; }

        public Frame MenuFrame { get; set; }

        public Level Level
        {
            get { return level; }
            set { level = value; }
        }

        public TrainDate TrainDate
        {
            get { return trainDate; }
            set { trainDate = value; }
        }

        public string ThemePath
        {
            get { return themePath; }
            set
            {
                themePath = value;
            }
        }

        public string LanguagePath
        {
            get { return languagePath; }
            set
            {
                languagePath = value;
            }
        }
        public string Language
        {
            get { return language; }
            set
            {
                language = value;
            }
        }

        public string Theme
        {
            get { return theme; }
            set
            {
                theme = value;
            }
        }

        public string PathNoImage
        {
            get { return pathNoImage; }
            set
            {
                pathNoImage = value;
            }
        }
    }
}
