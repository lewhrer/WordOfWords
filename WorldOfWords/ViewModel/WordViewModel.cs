using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using WorldOfWords.Model;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorldOfWords.ViewModel
{
    public class WordViewModel : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Translate { get; set; }
        public string LastUpdate { get; set; }
        public string Example { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage sourcePicture;
        public BitmapImage SourcePictureCorectly { get; set; }
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
            Example = word.Example;
            if(word.Picture != null)
            {
                SourcePictureCorectly = GetSourceImage(word.Picture);
                SourcePicture = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/HideImage.png"));
            }
            else
            {
                SourcePictureCorectly = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/NotImage2.png"));
                SourcePicture = SourcePictureCorectly;
            }
        }

        public BitmapImage SourcePicture
        {
            get { return sourcePicture; }
            set
            {
                sourcePicture = value;
                OnPropertyChanged("SourcePicture");
            }
        }


        public BitmapImage GetSourceImage(byte[] array)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                stream.Write(array, 0, array.Length);
                stream.Position = 0;
            }
            catch (Exception e)
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
