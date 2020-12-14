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
using WorldOfWords.Model;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    /// <summary>
    /// Interaction logic for EditTheme.xaml
    /// </summary>
    public partial class EditTheme : Page
    {
        public EditTheme(Theme tm)
        {
            InitializeComponent();
            DataContext = new EditThemeViewModel(tm);
        }
    }
}
