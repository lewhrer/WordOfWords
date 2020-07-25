using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;

namespace WorldOfWords.Tests
{
    [TestClass]
    public class WordServiceTest
    {
        [TestMethod]
        public void DatabaseReturnsAllCountOfWords()
        {
            var mock = new Mock<IWordService>();
            mock.Setup(repo => repo.GetAllWords()).Returns(GetTestWords());
            var service = mock.Object;

            Assert.AreEqual(GetTestWords().Count, service.GetAllWords().Count);
        }

        private List<Word> GetTestWords()
        {
            var users = new List<Word>
            {
                new Word { Id = new Guid("2432fe24-3a95-4bfe-85bd-4191cd41b230"), Name = "Samsung", Translate = "Самсунг" },
                new Word { Id = new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), Name = "Samsung2", Translate = "Самсунг2" },
                new Word { Id = new Guid("1f444245-51e5-452a-bdab-6010e82b8937"), Name = "Samsung3", Translate = "Самсунг3" },
                new Word { Id = new Guid("a3ced0f8-7d7a-4442-871e-5bcd401e74a3"), Name = "Samsung4", Translate = "Самсунг4" },
            };
            return users;
        }
    }
}
