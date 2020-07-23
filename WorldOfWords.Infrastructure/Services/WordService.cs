using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Model;
using System.Data.Entity;
using System.Windows;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace WorldOfWords.Infrastructure.Services
{
    public class WordService : IWordService
    {
        private WorldOfWordsDbContext _context;

        public WordService(WorldOfWordsDbContext context)
        {
            _context = context;
        }

        public void Create(WordArgs args)
        {
            var word = new Word()
            {
                Id = Guid.NewGuid(),
                Example = args.Example,
                Name = args.Name,
                Picture = args.Picture,
                Level = args.Level,
                Translate = args.Translate,
                LastUpdate = args.LastUpdate,
                Priority = args.Priority
            };

            _context.Words.Add(word);
            _context.SaveChanges();
        }

        public void AddRange(Word[] words)
        {
            foreach (var item in words)
            {
                item.Id = Guid.NewGuid();
                _context.Words.Add(item);
            }

            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var word = GetWord(id);
            _context.Words.Remove(word);
            _context.SaveChanges();
        }

        public void DeleteEverything()
        {
            _context.Words.RemoveRange(GetAllWords());
            _context.SaveChanges();
        }

        public void Edit(WordArgs args)
        {
            var word = GetWord(args.Id);
            word.Example = args.Example;
            word.Name = args.Name;
            word.Picture = args.Picture;
            word.Level = args.Level;
            word.Translate = args.Translate;
            word.Priority = args.Priority;

            _context.SaveChanges();
        }

        public byte[] FindImage()
        {
            byte[] array;
            FileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            try
            {
                fd.Filter = "Image File (*.jpg;*.bmp;*.gif;*.png;*.jpeg)|*.jpg;*.bmp;*.gif;*.png;*.jpeg";
                fd.ShowDialog();
                {
                    using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        array = new byte[fs.Length];
                        fs.Read(array, 0, Convert.ToInt32(fs.Length));
                    }
                }
                return array;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public List<Word> GetWords(int bottomLine, int topLine)
        {
            return _context.Words.Where(x => x.Level >= bottomLine && x.Level <= bottomLine).OrderByDescending(x => x.Priority).ToList();
        }

        public List<Word> GetAllWords()
        {
            var dateNow = DateTime.Now;
            
            return _context.Words.AsEnumerable().OrderByDescending(x => x.Priority).ThenByDescending(x => (dateNow - x.LastUpdate).Days ).ToList();
        }

        public BitmapImage GetSourceImage(byte[] array)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                stream.Write(array, 0, array.Length);
                stream.Position = 0;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

            Image img = Image.FromStream(stream);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }

        public List<Word> GetTrainWords(int bottomLine, int topLine, int days)
        {
            var dateNow = DateTime.Now;
            return _context.Words.AsEnumerable().Where(x => x.Level >= bottomLine && x.Level <= topLine && (dateNow - x.LastUpdate).Days >= days).OrderByDescending(x => x.Priority).ToList();
        }

        public List<Word> GetTrainAllWords()
        {
            var dateNow = DateTime.Now;
            return _context.Words.AsEnumerable().OrderByDescending(x => x.Priority).ThenByDescending(x => (dateNow - x.LastUpdate).Days).ToList();
        }

        public Word GetWord(string id)
        {
            return _context.Words.FirstOrDefault(x => x.Id.ToString() == id);
        }

        public void SetKnow(string id, int percent)
        {
            var word = GetWord(id);
            word.Level = (word.Level + percent) / 2;
            word.LastUpdate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
