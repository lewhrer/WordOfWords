using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
using WorldOfWords.Model;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class ListOfWords : Page
    {
        public ListOfWordsViewModel ViewModel { get; set; }
        
        public ListOfWords(string nameMethod, string namePage, string nameTrainPage)
        {
            InitializeComponent();
            ViewModel = new ListOfWordsViewModel(nameMethod, namePage, nameTrainPage);
            DataContext = ViewModel;
        }
    }
}
