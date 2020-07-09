using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorldOfWords
{
    public class TrainDate : INotifyPropertyChanged
    {
        private int unknown;
        private int notMemorized;
        private int memorized;
        private int learned;

        public TrainDate()
        {
            Unknown = 1;
            NotMemorized = 2;
            Memorized = 4;
            Learned = 10;
        }

        public int Unknown
        {
            get { return unknown; }
            set
            {
                unknown = value;
                OnPropertyChanged("Unknown");
            }
        }

        public int NotMemorized
        {
            get { return notMemorized; }
            set
            {
                notMemorized = value;
                OnPropertyChanged("NotMemorized");
            }
        }

        public int Memorized
        {
            get { return memorized; }
            set
            {
                memorized = value;
                OnPropertyChanged("Memorized");
            }
        }

        public int Learned
        {
            get { return learned; }
            set
            {
                learned = value;
                OnPropertyChanged("Learned");
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
