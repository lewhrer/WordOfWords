using System;
using System.IO;
using System.Windows;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using WorldOfWords.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace WorldOfWords.ViewModel
{
    public class WordViewModel : BaseViewModel
    {
        private string name;
        private string translate;
        private string example;
        private string hidedTranslate;
        private string hidedExample;
        private string hidedPicture;
        private ComboBoxItem wordPriority;

        public Guid Id { get; set; }
        public string LastUpdate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string ExampleCorectly { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage sourcePicture;
        public BitmapImage SourcePictureCorectly { get; set; }
        public BitmapImage sourcePictureExample { get; set; }
        public int Level { get; set; }
        public int Priority { get; set; }
        public ObservableCollection<ComboBoxItem> Priorities { get; set; }
        public Brush RowColor { get; set; }

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

        public WordViewModel(Word word, int index)
        {
            Id = word.Id;
            Name = word.Name;
            Translate = word.Translate;
            HidedTranslate = "...???...";
            LastUpdate = word.LastUpdate.ToShortDateString();
            Level = word.Level;
            Priority = word.Priority;
            Picture = word.Picture;
            ExampleCorectly = word.Example;

            if (word.Example != null)
            {
                HidedExample = "...???...";
            }

            if(word.Picture != null)
            {
                HidedPicture = "...???...";
                SourcePictureCorectly = GetSourceImage(word.Picture);
            }

            if(index % 2 == 0)
            {
                RowColor = new SolidColorBrush(Colors.Black);
            }
            else
            {
                RowColor = null;
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

        public string HidedTranslate
        {
            get { return hidedTranslate; }
            set
            {
                hidedTranslate = value;
                OnPropertyChanged(nameof(HidedTranslate));
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

        public string HidedExample
        {
            get { return hidedExample; }
            set
            {
                hidedExample = value;
                OnPropertyChanged("HidedExample");
            }
        }

        public string HidedPicture
        {
            get { return hidedPicture; }
            set
            {
                hidedPicture = value;
                OnPropertyChanged("HidedPicture");
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
    }
}
