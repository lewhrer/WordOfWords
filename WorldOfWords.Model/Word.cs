using System;
using System.Collections.Generic;

namespace WorldOfWords.Model
{
    public class Word
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TranslateName { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<string> Examples { get; set; }
        public ICollection<double> Statuses { get; set; }
    }
}
