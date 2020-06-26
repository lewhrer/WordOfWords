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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldOfWords.Infrastructure;
using WorldOfWords.Infrastructure.Services;
using WorldOfWords.Model;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class ListOfWords : Page
    {
        public ListOfWords(Frame menuFrame, IWordService service, List<Word> words)
        {
            InitializeComponent();
            DataContext = new ListOfWordsViewModel(menuFrame, service, words);
        }
    }
}