using System;
using System.Runtime.Serialization;

namespace WorldOfWords.Model
{
    public class Word
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Translate { get; set; }
        public byte[] Picture { get; set; }
        public string Example { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public int Level { get; set; }
        public int Priority { get; set; }

        public Guid ThemeId { get; set; }

        public Theme Theme { get; set; }
    }
}