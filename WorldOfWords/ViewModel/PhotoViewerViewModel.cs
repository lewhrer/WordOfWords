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
        Frame photoFrame;
        public BitmapImage SourceImage { get; set; }

        public PhotoViewerViewModel(Frame photoFrame, BitmapImage sourceImage)
        {
            SourceImage = sourceImage;
            this.photoFrame = photoFrame;
        }

        private RelayCommand mouseDownOnBlackAreaCommand;
        public RelayCommand MouseDownOnBlackAreaCommand
        {
            get
            {
                return mouseDownOnBlackAreaCommand ??
                  (mouseDownOnBlackAreaCommand = new RelayCommand(obj =>
                  {
                      photoFrame.GoBack();
                  }));
            }
        }
    }
}
