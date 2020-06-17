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
        Task Create(WordArgs args);
        Task<Word> GetWord(string id);
        Task<List<Word>> GetAllWords();
        Task<List<Word>> GetStudiedWords();
        Task<List<Word>> GetNotStudiedWords();
        Task<List<Word>> GetAlmostStudiedWords();
        Task<List<Word>> GetAcquaintedWords();
        Task Edit(WordArgs args);
        Task Delete(string id);
    }
}
