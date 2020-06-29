using System;
using System.Runtime.Serialization;

namespace WorldOfWords.Model
{
    [DataContract]
    public class Word
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string TranslateName { get; set; }
        [DataMember]
        public byte[] Picture { get; set; }
        [DataMember]
        public string Example { get; set; }
        [DataMember]
        public virtual DateTime LastUpdate { get; set; }
        [DataMember]
        public double Level { get; set; }
        [DataMember]
        public int Priority { get; set; }
    }
}
