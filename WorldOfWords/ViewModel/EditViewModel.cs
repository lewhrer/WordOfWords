﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        IUpdater updater;

        private readonly string id;
        private string name;
        private string translate;
        private string example;
        private readonly int level;
        private readonly DateTime lastUpdate;
        private ComboBoxItem priority;
        public ObservableCollection<ComboBoxItem> Priorities { get; set; }

        public EditViewModel(Frame menuFrame, IWordService wordService, Image picture, string id, IUpdater updater)
        {
            _menuFrame = menuFrame;
            _wordService = wordService;
            this.picture = picture;
            this.id = id;
            this.updater = updater;

            var word = _wordService.GetWord(id);
            Name = word.Name;
            Translate = word.Translate;
            Example = word.Example;
            level = word.Level;
            lastUpdate = word.LastUpdate;
            Priorities = new ObservableCollection<ComboBoxItem>()
            {
                new ComboBoxItem(){ Content = "0" },
                new ComboBoxItem(){ Content = "1" },
                new ComboBoxItem(){ Content = "2" },
            };

            Priority = Priorities[Convert.ToInt32(word.Priority)];

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
                          LastUpdate = lastUpdate,
                          Translate = Translate,
                          Level = level,
                          Priority = int.Parse(Priority.Content.ToString()),
                      };

                      _wordService.Edit(args);
                      
                      MessageBox.Show("Sacsesfull edited!");
                      updater.Update();
                      _menuFrame.GoBack();
                      //_menuFrame.Navigate(new ListOfWords(_menuFrame, _wordService, _wordService.GetAllWords()));
                  }));
            }
        }

        private RelayCommand goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return goBackCommand ??
                  (goBackCommand = new RelayCommand(obj =>
                  {
                      updater.Update();
                      _menuFrame.GoBack();
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

        public ComboBoxItem Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                OnPropertyChanged("Priority");
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
