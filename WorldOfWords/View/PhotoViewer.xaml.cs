using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class PhotoViewer : Page
    {
        public PhotoViewer(BitmapImage sourceImage)
        {
            InitializeComponent();
            DataContext = new PhotoViewerViewModel(sourceImage);
        }
    }
}
