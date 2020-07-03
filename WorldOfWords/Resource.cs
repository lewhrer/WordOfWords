using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorldOfWords
{
    static class Resource
    {
        public static readonly BitmapImage sourceNoImage;

        static Resource()
        {
            sourceNoImage = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/Image.png"));
        }
    }
}
