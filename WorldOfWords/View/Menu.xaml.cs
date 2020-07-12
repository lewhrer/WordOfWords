using System.Windows;
using System.Windows.Controls;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
            Resource.getInstance().MenuFrame = MenuFrame;
            DataContext = new MenuViewModel();
            MenuFrame.Navigate(new ListOfWords("All", Application.Current.Resources["AllWords"].ToString(), 
                Application.Current.Resources["TrainingAllWords"].ToString()));
        }

        public Menu(int someInt)
        {
            InitializeComponent();
            Resource.getInstance().MenuFrame = MenuFrame;
            DataContext = new MenuViewModel();
            Resource.getInstance().MenuFrame.Navigate(new Settings());
        }
    }
}
