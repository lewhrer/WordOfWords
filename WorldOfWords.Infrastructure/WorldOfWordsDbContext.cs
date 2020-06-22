using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WorldOfWords.Model;
using System.Windows;

namespace WorldOfWords.Infrastructure
{
    public class WorldOfWordsDbContext : DbContext
    {
        public DbSet<Word> Words { get; set; }

        static WorldOfWordsDbContext()
        {
            Database.SetInitializer(new MyContextInitializer());
        }

        public WorldOfWordsDbContext() : base("DbConnection")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Word>().Property(x => x.LastUpdate).IsOptional();
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
