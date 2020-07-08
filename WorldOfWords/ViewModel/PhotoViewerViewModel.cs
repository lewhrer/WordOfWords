using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WorldOfWords.ViewModel
{
    public class PhotoViewerViewModel
    {
        public BitmapImage SourceImage { get; set; }

        public PhotoViewerViewModel(BitmapImage sourceImage)
        {
            SourceImage = sourceImage;
        }

        private RelayCommand mouseDownOnBlackAreaCommand;
        public RelayCommand MouseDownOnBlackAreaCommand
        {
            get
            {
                return mouseDownOnBlackAreaCommand ??
                  (mouseDownOnBlackAreaCommand = new RelayCommand(obj =>
                  {
                      Resource.getInstance().MenuFrame.GoBack();
                  }));
            }
        }
    }
}
