using System.Runtime.Serialization;

namespace WorldOfWords
{
    [DataContract]
    public class Level
    {
        [DataMember]
        public int First { get; set; }
        [DataMember]
        public int Second { get; set; }
        [DataMember]
        public int Third { get; set; }

        public Level()
        {
            First = 10;
            Second = 50;
            Third = 90;
        }
    }
}
