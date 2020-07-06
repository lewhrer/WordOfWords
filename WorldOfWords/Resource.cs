using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;


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

        public Resource()
        {
            PathNoImage = "pack://application:,,,/WorldOfWords;component/Resources/Image.png";
            ThemePath = "pack://application:,,,/MahApps.Metro;component/Styles/Themes/Dark.Blue.xaml";
            Theme = "Dark";
        }

        public static Resource getInstance()
        {
            if (instance == null)
                instance = new Resource();
            return instance;
        }

        public BitmapImage SourceNoImage { get { return new BitmapImage(new Uri(pathNoImage)); } }

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
