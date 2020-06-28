using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Model;

namespace WorldOfWords.Infrastructure.Arguments
{
    public class WordArgs
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TranslateName { get; set; }
        public byte[] Picture { get; set; }
        public string Example { get; set; }
        public double Level { get; set; }
        public int Priority { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
