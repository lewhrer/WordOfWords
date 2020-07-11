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
using System.Windows.Shapes;
using WorldOfWords.Infrastructure;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.ViewModel;
using MahApps.Metro.Controls;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WorldOfWords.View
{
    public partial class Menu : MetroWindow
    {
        public Menu()
        {
            InitializeComponent();
            Resource.getInstance().MenuFrame = MenuFrame;
            DataContext = new MenuViewModel();
            MenuFrame.Navigate(new ListOfWords("All", Application.Current.Resources["AllWords"].ToString(), 
                Application.Current.Resources["TrainingAllWords"].ToString()));
        }
    }
}
