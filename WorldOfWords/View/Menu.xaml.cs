﻿using System.Windows;
using System.Windows.Controls;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class Menu : Page
    {
        public static Frame Frame { get; set; }

        public Menu()
        {
            InitializeComponent();
            Frame = MenuFrame;
            DataContext = new MenuViewModel();
            Frame.Navigate(new ListOfThemes());
        }

        public Menu(int someInt)
        {
            InitializeComponent();
            Frame = MenuFrame;
            DataContext = new MenuViewModel();
            Frame.Navigate(new Settings());
        }
    }
}
