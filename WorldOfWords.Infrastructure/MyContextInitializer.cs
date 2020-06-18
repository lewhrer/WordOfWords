using System;
using System.Data.Entity;
using WorldOfWords.Model;

namespace WorldOfWords.Infrastructure
{
    class MyContextInitializer : DropCreateDatabaseAlways<WorldOfWordsDbContext>
    {
        protected override void Seed(WorldOfWordsDbContext db)
        {
            Word w1 = new Word { Id = Guid.NewGuid(), Name = "Samsung", TranslateName = "Самсунг" };
            Word w2 = new Word { Id = Guid.NewGuid(), Name = "Samsung2", TranslateName = "Самсунг2" };
            Word w3 = new Word { Id = Guid.NewGuid(), Name = "Samsung3", TranslateName = "Самсунг3" };
            Word w4 = new Word { Id = Guid.NewGuid(), Name = "Samsung4", TranslateName = "Самсунг4" };

            db.Words.Add(w1);
            db.Words.Add(w2);
            db.Words.Add(w3);
            db.Words.Add(w4);
            db.SaveChanges();
        }
    }
}
