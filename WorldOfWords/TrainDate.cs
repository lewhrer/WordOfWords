using System.Runtime.Serialization;

namespace WorldOfWords
{
    [DataContract]
    public class TrainDate
    {
        [DataMember]
        public int Unknown { get; set; }
        [DataMember]
        public int NotMemorized { get; set; }
        [DataMember]
        public int Memorized { get; set; }
        [DataMember]
        public int Learned { get; set; }

        public TrainDate()
        {
            Unknown = 1;
            NotMemorized = 2;
            Memorized = 4;
            Learned = 10;
        }
    }
}
