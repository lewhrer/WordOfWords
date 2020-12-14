using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Model;

namespace WorldOfWords.Infrastructure.Arguments
{
    public class ThemeArgs
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Word> Words { get; set; }
    }
}
