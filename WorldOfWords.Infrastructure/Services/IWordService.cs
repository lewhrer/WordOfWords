using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Model;

namespace WorldOfWords.Infrastructure.Services
{
    public interface IWordService
    {
        void Create(WordArgs args);
        Word GetWord(string id);
        List<Word> GetAllWords();
        List<Word> GetStudiedWords();
        List<Word> GetNotStudiedWords();
        List<Word> GetAlmostStudiedWords();
        List<Word> GetAcquaintedWords();
        void Edit(WordArgs args);
        void Delete(string id);
    }
}
