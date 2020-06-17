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

        public async Task Create(WordArgs args)
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
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var word = await GetWord(id);
            _context.Words.Remove(word);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(WordArgs args)
        {
            var word = await GetWord(args.Id);
            word.Examples = args.Examples;
            word.Name = args.Name;
            word.Picture = args.Picture;
            word.Statuses = args.Statuses;
            word.TranslateName = args.TranslateName;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Word>> GetAcquaintedWords()
        {
            return await _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count > 25
                && x.Statuses.Sum() / x.Statuses.Count <= 50).ToListAsync();
        }

        public async Task<List<Word>> GetAlmostStudiedWords()
        {
            return await _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count > 50 
                && x.Statuses.Sum() / x.Statuses.Count <= 75).ToListAsync();
        }

        public async Task<List<Word>> GetAllWords()
        {
            return await _context.Words.ToListAsync();
        }

        public async Task<List<Word>> GetNotStudiedWords()
        {
            return await _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count < 25).ToListAsync();
        }

        public async Task<List<Word>> GetStudiedWords()
        {
            return await _context.Words.Where(x => x.Statuses.Sum() / x.Statuses.Count > 75).ToListAsync();
        }

        public async Task<Word> GetWord(string id)
        {
            return await _context.Words.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
