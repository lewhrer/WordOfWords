﻿using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WorldOfWords.Model;
using WorldOfWords.ViewModel;

namespace WorldOfWords.View
{
    public partial class Create : Page
    {
        public Create(IUpdater updater = null)
        {
            InitializeComponent();
            DataContext = new CreateViewModel(updater, this);
        }
        public Create(Theme the, IUpdater updater = null)
        {
            InitializeComponent();
            DataContext = new CreateViewModel(the, updater, this);
        }

        public async void ActionResult(Brush color, string text)
        {
            TbkResult.Foreground = color;
            TbkResult.Text = text;
            TbkResult.Opacity = 1;
            await Task.Delay(500);

            for (double i = 1; i >= 0; i = i - 0.05)
            {
                await Task.Delay(100);
                TbkResult.Opacity = i;
            }
        }
    }
}
