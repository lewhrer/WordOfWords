using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace WorldOfWords.View
{
    public partial class MainWindow : MetroWindow
    {
        public static Frame Frame { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Menu());
            Frame = MainFrame;
        }
    }
}
