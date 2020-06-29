using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Model;

namespace WorldOfWords.Infrastructure.Services
{
    public interface IWordService
    {
        void AddRange(Word[] words);
        void Create(WordArgs args);
        Word GetWord(string id);
        List<Word> GetAllWords();
        List<Word> GetStudiedWords();
        List<Word> GetNotStudiedWords();
        List<Word> GetAlmostStudiedWords();
        List<Word> GetAlmostAcquaintedWords();
        List<Word> GetAcquaintedWords();
        List<Word> GetTrainAllWords();
        List<Word> GetTrainStudiedWords();
        List<Word> GetTrainNotStudiedWords();
        List<Word> GetTrainAlmostStudiedWords();
        List<Word> GetTrainAlmostAcquaintedWords();
        List<Word> GetTrainAcquaintedWords();
        List<Word> GetTrainNewWords();
        void Edit(WordArgs args);
        void Delete(string id);
        void DeleteEverything();
        byte[] FindImage();
        BitmapImage GetSourceImage(byte[] array);
        void SetKnow(string id, double percent);
    }
}
