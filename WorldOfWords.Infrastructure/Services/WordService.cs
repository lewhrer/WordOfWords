using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Model;
using System.Data.Entity;

namespace WorldOfWords.Infrastructure.Services
{
    public class WordService : IWordService
    {
        private WorldOfWordsDbContext _context;

        public WordService(WorldOfWordsDbContext context)
        {
            _context = context;
        }

        public void Create(WordArgs args)
        {
            var word = new Word()
            {
                Id = Guid.NewGuid(),
                Examples = args.Examples,
                Name = args.Name,
                Picture = args.Picture,
                Statuses = args.Statuses,
                TranslateName = args.TranslateName,
            };

            _context.Words.Add(word);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var word = GetWord(id);
            _context.Words.Remove(word);
            _context.SaveChanges();
        }

        public void Edit(WordArgs args)
        {
            var word = GetWord(args.Id);
            word.Examples = args.Examples;
            word.Name = args.Name;
            word.Picture = args.Picture;
            word.Statuses = args.Statuses;
            word.TranslateName = args.TranslateName;

            _context.SaveChanges();
        }

        public List<Word> GetAcquaintedWords()
        {
            return _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count > 25
                && x.Statuses.Sum() / x.Statuses.Count <= 50).ToList();
        }

        public List<Word> GetAlmostStudiedWords()
        {
            return _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count > 50 
                && x.Statuses.Sum() / x.Statuses.Count <= 75).ToList();
        }

        public List<Word> GetAllWords()
        {
            return _context.Words.ToList();
        }

        public List<Word> GetNotStudiedWords()
        {
            return _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count < 25).ToList();
        }

        public List<Word> GetStudiedWords()
        {
            return _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count > 75).ToList();
        }

        public Word GetWord(string id)
        {
            return _context.Words.FirstOrDefault(x => x.Id.ToString() == id);
        }
    }
}
