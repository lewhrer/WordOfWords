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
                TranslateName = args.TranslateName,
            };

            _context.Words.Add(word);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var word = GetWord(id);
            _context.Words.Remove(word);
            _context.SaveChanges();
        }

        public void Edit(WordArgs args)
        {
            var word = GetWord(args.Id);
            word.Example = args.Example;
            word.Name = args.Name;
            word.Picture = args.Picture;
            word.Level = args.Level;
            word.TranslateName = args.TranslateName;

            _context.SaveChanges();
        }

        public List<Word> GetAcquaintedWords()
        {
            return _context.Words.Where(x => x.Level >= 50 && x.Level < 75).ToList();
        }

        public List<Word> GetAlmostStudiedWords()
        {
            return _context.Words.Where(x => x.Level >= 75 && x.Level < 90).ToList();
        }

        public List<Word> GetAlmostAcquaintedWords()
        {
            return _context.Words.Where(x => x.Level >= 25 && x.Level < 50).ToList();
        }

        public List<Word> GetAllWords()
        {
            return _context.Words.ToList();
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
                    //picture.Source = new BitmapImage(new Uri(fd.FileName));
                }
                //fd = null;
                return array;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public List<Word> GetNotStudiedWords()
        {
            return _context.Words.Where(x => x.Level < 25).ToList();
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

        public List<Word> GetStudiedWords()
        {
            return _context.Words.Where(x => x.Level >= 90).ToList();
        }

        public Word GetWord(string id)
        {
            return _context.Words.FirstOrDefault(x => x.Id.ToString() == id);
        }

        public void SetKnow(string id, double percent)
        {
            var word = GetWord(id);
            word.Level = Math.Round((word.Level + percent) / 2, 2);
            _context.SaveChanges();
        }
    }
}
