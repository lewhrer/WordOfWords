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

        public Resource()
        {
            PathNoImage = "pack://application:,,,/WorldOfWords;component/Resources/Image.png";
            ThemePath = "pack://application:,,,/MahApps.Metro;component/Styles/Themes/Dark.Blue.xaml";
            Theme = "Dark";
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

        public Level Level { get; set; }

        public TrainDate TrainDate { get; set; }

        public string ThemePath
        {
            get { return themePath; }
            set
            {
                themePath = value;
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
