using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldOfWords.ViewModel;

namespace WorldOfWords.Tests
{
    [TestClass]
    public class CreateViewModelTest
    {
        [TestMethod]
        public void WordArgsMustBeEqualNewWordWhenCallMethodCreate()
        {
            var model = new CreateViewModel(null, null);
            model.NewWord.Name = "name";
            model.NewWord.Translate = "ім'я";
            model.NewWord.Example = "Example";
            model.NewWord.Picture = new byte[1000];
            model.NewWord.WordPriority = model.NewWord.Priorities[1];

            var args = model.CreateWordArgs();

            Assert.AreEqual(args.Name, model.NewWord.Name);
            Assert.AreEqual(args.Translate, model.NewWord.Translate);
            Assert.AreEqual(args.Example, model.NewWord.Example);
            Assert.AreEqual(args.Picture, model.NewWord.Picture);
            Assert.AreEqual(args.Level, model.NewWord.Level);
            Assert.AreEqual(args.Priority, int.Parse(model.NewWord.WordPriority.Content.ToString()));
        }

        [TestMethod]
        public void NewWordMustBeEmptyWhenCallMethodReset()
        {
            var model = new CreateViewModel(null, null);
            model.NewWord.Name = "name";
            model.NewWord.Translate = "ім'я";

            model.ResetNewWord();

            Assert.AreEqual(model.NewWord.Name, null);
            Assert.AreEqual(model.NewWord.Translate, null);
            Assert.AreEqual(model.NewWord.SourcePicture.UriSource.ToString(), Resource.getInstance().SourceNoImage.UriSource.ToString());
            Assert.AreEqual(model.NewWord.Example, null);
            Assert.AreEqual(model.NewWord.WordPriority, model.NewWord.Priorities[0]);
        }

        [TestMethod]
        public void PictureMustBeDeletedWhenCallMethodDelete()
        {
            var model = new CreateViewModel(null, null);

            model.DeletePicture();

            Assert.AreEqual(model.NewWord.Picture, null);
            Assert.AreEqual(model.NewWord.SourcePicture.UriSource.ToString(), Resource.getInstance().SourceNoImage.UriSource.ToString());
        }

        [TestMethod]
        public void MustBeReturnTrueWhenNameIsNotEntered()
        {
            var model = new CreateViewModel(null, null);

            Assert.IsTrue(model.IsEnteredName());
        }

        [TestMethod]
        public void MustBeReturnFalseWhenNameIsEntered()
        {
            var model = new CreateViewModel(null, null);
            model.NewWord.Name = "name";

            Assert.IsFalse(model.IsEnteredName());
        }

        [TestMethod]
        public void MustBeReturnTrueWhenTranslateIsNotEntered()
        {
            var model = new CreateViewModel(null, null);

            Assert.IsTrue(model.IsEnteredTranslate());
        }

        [TestMethod]
        public void MustBeReturnFalseWhenTranslateIsEntered()
        {
            var model = new CreateViewModel(null, null);
            model.NewWord.Translate = "translate";

            Assert.IsFalse(model.IsEnteredTranslate());
        }
    }
}
