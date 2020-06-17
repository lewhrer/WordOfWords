using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfWords.Infrastructure.Arguments
{
    public class WordArgs
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TranslateName { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<string> Examples { get; set; }
        public ICollection<double> Statuses { get; set; }
    }
}
