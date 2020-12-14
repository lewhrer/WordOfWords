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
    /// Interaction logic for CreateTheme.xaml
    /// </summary>
    public partial class CreateTheme : Page
    {
        public CreateTheme()
        {
            InitializeComponent();
            DataContext = new CreateThemeViewModel();
        }
    }
}
