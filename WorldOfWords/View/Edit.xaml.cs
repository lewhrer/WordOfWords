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
using WorldOfWords.Infrastructure;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        public Edit(Frame menuFrame, string id)
        {
            InitializeComponent();
            DataContext = new EditViewModel(menuFrame, new WordService(new WorldOfWordsDbContext()), picture, id);
        }
    }
}
