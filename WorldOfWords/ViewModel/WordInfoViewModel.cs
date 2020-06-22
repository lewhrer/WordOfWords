using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;

namespace WorldOfWords.ViewModel
{
    public class WordInfoViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        Word word;
        Image picture;

        public IWordService _wordService;
        public ObservableCollection<Word> Words { get; set; }

        public WordInfoViewModel(Frame menuFrame, IWordService wordService, List<Word> words, int indexWord, Image picture)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            Words = new ObservableCollection<Word>(words);
            Word = Words[indexWord];
            this.picture = picture;
            if(Word.Picture != null)
            {
                picture.Source = _wordService.GetSourceImage(Word.Picture);
            }
        }

        public Word Word
        {
            get { return word; }
            set
            {
                word = value;
                OnPropertyChanged("Word");
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
