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
        public string Example { get; set; }
        public virtual DateTime? LastUpdate { get; set; }
        public double Level { get; set; }
        public int Priority { get; set; }
    }
}
