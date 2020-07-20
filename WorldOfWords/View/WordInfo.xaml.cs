using System.Collections.Generic;
using System.Windows.Controls;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class WordInfo : Page
    {
        public WordInfoViewModel ViewModel { get; set; }

        public WordInfo(List<WordViewModel> words, string namePage, IUpdater updater = null, int indexWord = 0)
        {
            InitializeComponent();
            ViewModel = new WordInfoViewModel(words, updater, namePage, indexWord);
            DataContext = ViewModel;
        }
    }
}
