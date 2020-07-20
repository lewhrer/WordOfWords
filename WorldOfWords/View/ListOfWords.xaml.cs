using System.Windows.Controls;
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
