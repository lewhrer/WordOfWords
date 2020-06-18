using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;

namespace WorldOfWords.ViewModel
{
    public class AllWordsViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        Word selectedWord;

        public IWordService _wordService;
        public ObservableCollection<Word> Words { get; set; }

        public AllWordsViewModel(Frame menuFrame, IWordService wordService)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            Words = new ObservableCollection<Word>(_wordService.GetAllWords());
            //InitializeOfWords();
        }

        //private async void InitializeOfWords()
        //{
        //    Words = new ObservableCollection<Word>(await _wordService.GetAllWords());
        //}

        public Word SelectedWord
        {
            get { return selectedWord; }
            set
            {
                selectedWord = value;
                OnPropertyChanged("SelectedWord");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
