using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings(Frame menuFrame, IWordService service)
        {
            InitializeComponent();
            DataContext = new SettingsViewModel(menuFrame, service);
        }
    }
}
