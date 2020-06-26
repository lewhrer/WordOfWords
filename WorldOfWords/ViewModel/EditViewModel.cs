using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WorldOfWords.Infrastructure.Arguments;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.View;

namespace WorldOfWords.ViewModel
{
    public class EditViewModel : INotifyPropertyChanged
    {
        Frame _menuFrame;
        Image picture;
        byte[] pictureInBytes;

        public IWordService _wordService;

        private readonly string id;
        private string name;
        private string translate;
        private string example;
        private double level;

        public EditViewModel(Frame menuFrame, IWordService wordService, Image picture, string id)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            this.picture = picture;
            this.id = id;

            var word = _wordService.GetWord(id);
            Name = word.Name;
            Translate = word.TranslateName;
            Example = word.Example;
            level = word.Level;

            if(word.Picture != null)
            {
                pictureInBytes = word.Picture;
                this.picture.Source = _wordService.GetSourceImage(word.Picture);
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      pictureInBytes = _wordService.FindImage();
                      picture.Source = _wordService.GetSourceImage(pictureInBytes);
                  }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      pictureInBytes = null;
                      try
                      {
                          picture.Source = new BitmapImage(new Uri("pack://application:,,,/WorldOfWords;component/Resources/temp.jpg"));
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      var args = new WordArgs()
                      {
                          Id = id,
                          Example = Example,
                          Name = Name,
                          Picture = pictureInBytes,
                          LastUpdate = DateTime.Now,
                          TranslateName = Translate,
                          Level = level,
                      };

                      _wordService.Edit(args);
                      
                      MessageBox.Show("Sacsesfull edited!");
                      _menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetAllWords()));
                  }));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
