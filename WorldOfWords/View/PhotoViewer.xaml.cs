using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    /// <summary>
    /// Interaction logic for PhotoViewer.xaml
    /// </summary>
    public partial class PhotoViewer : Page
    {
        public PhotoViewer(Frame photoFrame, BitmapImage sourceImage)
        {
            InitializeComponent();
            DataContext = new PhotoViewerViewModel(photoFrame, sourceImage);
        }
    }
}
