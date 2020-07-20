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
                      View.Menu.Frame.NavigationService.GoBack();
                  }));
            }
        }
    }
}
