using System.Windows.Controls;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class Settings : Page
    {
        public static Frame Details { get; set; }
        public Settings()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
            Details = DetailsFrame;
        }
    }
}
