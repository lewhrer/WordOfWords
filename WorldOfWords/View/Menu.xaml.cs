﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldOfWords.Infrastructure;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.ViewModel;
using MahApps.Metro.Controls;

namespace WorldOfWords.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : MetroWindow
    {
        public Menu()
        {
            InitializeComponent();
            var service = new WordService(new WorldOfWordsDbContext());
            DataContext = new MenuViewModel(MenuFrame, service);
            MenuFrame.Navigate(new ListOfWords(MenuFrame, service, "All", "all words"));
        }
    }
}
