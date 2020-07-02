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
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WorldOfWords.ViewModel
{
    public class WordViewModel : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string translate { get; set; }
        public string LastUpdate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string example;
        public string ExampleCorectly { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage sourcePicture;
        public BitmapImage SourcePictureCorectly { get; set; }
        public BitmapImage sourcePictureExample { get; set; }
        public bool IsPhoto { get; set; } = false;
        public int Level { get; set; }
        public int Priority { get; set; }
        public ObservableCollection<ComboBoxItem> Priorities { get; set; }
        private ComboBoxItem wordPriority;

        public WordViewModel(int priority = 0)
        {
            Priorities = new ObservableCollection<ComboBoxItem>()
            {
                new ComboBoxItem(){ Content = "0" },
                new ComboBoxItem(){ Content = "1" },
                new ComboBoxItem(){ Content = "2" },
            };
            WordPriority = Priorities[priority];
        }

        public WordViewModel(Word word)
        {
            Id = word.Id;
            Name = word.Name;
            Translate = word.Translate;
            LastUpdate = word.LastUpdate.ToShortDateString();
            Level = word.Level;
            Priority = word.Priority;
            Picture = word.Picture;
            ExampleCorectly = word.Example;

            if (word.Example != null)
            {
                SourcePictureExample = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/HideImage.png"));
            }

            if(word.Picture != null)
            {
                IsPhoto = true;
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

        public BitmapImage SourcePictureExample
        {
            get { return sourcePictureExample; }
            set
            {
                sourcePictureExample = value;
                OnPropertyChanged("SourcePictureExample");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Translate
        {
            get { return translate; }
            set
            {
                translate = value;
                OnPropertyChanged("Translate");
            }
        }

        public string Example
        {
            get { return example; }
            set
            {
                example = value;
                OnPropertyChanged("Example");
            }
        }

        public ComboBoxItem WordPriority
        {
            get { return wordPriority; }
            set
            {
                wordPriority = value;
                OnPropertyChanged("WordPriority");
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

            var img = System.Drawing.Image.FromStream(stream);
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
