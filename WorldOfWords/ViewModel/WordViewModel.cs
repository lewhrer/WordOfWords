using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Model;

namespace WorldOfWords.ViewModel
{
    public class WordViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Translate { get; set; }
        public string LastUpdate { get; set; }
        public byte[] Picture { get; set; }
        public int Level { get; set; }
        public int Priority { get; set; }

        public WordViewModel(Word word)
        {
            Id = word.Id;
            Name = word.Name;
            Translate = word.Translate;
            LastUpdate = word.LastUpdate.ToShortDateString();
            Level = word.Level;
            Priority = word.Priority;
            Picture = word.Picture;
        }
    }
}
