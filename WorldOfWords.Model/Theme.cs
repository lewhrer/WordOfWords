﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfWords.Model
{
    public class Theme
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Word> Words { get; set; }
    }
}
