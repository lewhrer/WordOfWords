using MahApps.Metro.Controls;

namespace WorldOfWords.View
{
    public partial class Warning : MetroWindow
    {
        public Warning(string message)
        {
            InitializeComponent();
            MainTextBlock.Text = message;
        }
    }
}
