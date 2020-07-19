using System.Windows.Controls;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class Details : Page
    {
        public Details(string message, Func func)
        {
            InitializeComponent();
            DataContext = new DetailsViewModel(message, func);
        }
    }
}
