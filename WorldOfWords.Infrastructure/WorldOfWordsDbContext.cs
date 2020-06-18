﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WorldOfWords.Model;

namespace WorldOfWords.Infrastructure
{
    public class WorldOfWordsDbContext : DbContext
    {
        public DbSet<Word> Words { get; set; }

        //static WorldOfWordsDbContext()
        //{
        //    Database.SetInitializer(new MyContextInitializer());
        //}

        public WorldOfWordsDbContext()
        : base("DbConnection")
        { }
    }
}
