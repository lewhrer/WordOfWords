using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorldOfWords
{
    public class Level : INotifyPropertyChanged
    {
        private int first;
        private int second;
        private int third;

        public Level()
        {
            First = 10;
            Second = 50;
            Third = 90;
        }

        public int First
        {
            get { return first; }
            set
            {
                first = value;
                OnPropertyChanged("First");
            }
        }

        public int Second
        {
            get { return second; }
            set
            {
                second = value;
                OnPropertyChanged("Second");
            }
        }

        public int Third
        {
            get { return third; }
            set
            {
                third = value;
                OnPropertyChanged("Third");
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
