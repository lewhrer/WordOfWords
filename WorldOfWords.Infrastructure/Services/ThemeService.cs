using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Model;

namespace WorldOfWords.Infrastructure.Services
{
    public class ThemeService
    {
        private WorldOfWordsDbContext _context;

        public ThemeService(WorldOfWordsDbContext context)
        {
            _context = context;
        }

        public async Task Create(ThemeArgs args)
        {
            var theme = new Theme()
            {
                Id = Guid.NewGuid(),
                Name = args.Name,
            };

            _context.Themes.Add(theme);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var theme = GetTheme(id);
            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();
        }

        public void Edit(ThemeArgs args)
        {
            var theme = GetTheme(args.Id);
            theme.Name = args.Name;

            _context.SaveChangesAsync();
        }

        public List<Theme> GetThemes()
        {
            return _context.Themes.Include("Words").ToList();
        }

        public Theme GetTheme(string id)
        {
            return _context.Themes.Include("Words").AsEnumerable().FirstOrDefault(x => x.Id.ToString() == id);
        }
    }
}
