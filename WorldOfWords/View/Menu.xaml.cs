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

namespace WorldOfWords.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            var service = new WordService(new WorldOfWordsDbContext());
            DataContext = new MenuViewModel(MenuFrame, service);
            using (var context = new WorldOfWordsDbContext())
            {
                MenuFrame.Navigate(new ListOfWords(MenuFrame, service, context.Words.ToList()));
            }
        }
    }
}
