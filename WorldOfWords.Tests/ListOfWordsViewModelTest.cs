using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldOfWords.ViewModel;

namespace WorldOfWords.Tests
{
    [TestClass]
    public class ListOfWordsViewModelTest
    {
        [TestMethod]
        public void TheWordMustBeRemovedFromWordListWhenCallMethodDeleteWord()
        {
            var model = new ListOfWordsViewModel(null, null, null);
            model.Words.Add(new WordViewModel() { Name = "name", Translate = "translate", });
            model.Words.Add(new WordViewModel() { Name = "name2", Translate = "translate2", });
            model.Words.Add(new WordViewModel() { Name = "name3", Translate = "translate3", });
            model.SelectedWord = model.Words[1];
            model.TotalCount = model.Words.Count;
            model.DeleteWord();

            Assert.IsFalse(model.Words.Contains(model.SelectedWord));
        }

        [TestMethod]
        public void CountOfWordsMustBeDecreaseOnOneWhenCallMethodDeleteWord()
        {
            var model = new ListOfWordsViewModel(null, null, null);
            model.Words.Add(new WordViewModel() { Name = "name", Translate = "translate", });
            model.Words.Add(new WordViewModel() { Name = "name2", Translate = "translate2", });
            model.Words.Add(new WordViewModel() { Name = "name3", Translate = "translate3", });
            model.SelectedWord = model.Words[1];
            model.TotalCount = model.Words.Count;
            int oldCount = model.Words.Count;
            model.DeleteWord();

            Assert.AreEqual(model.Words.Count, oldCount - 1);
            Assert.AreEqual(model.Words.Count, model.TotalCount);
        }
    }
}
