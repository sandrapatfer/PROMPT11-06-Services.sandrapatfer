using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Service3
{
    [DataContract]
    public class WordReq
    {
        [DataMember]
        public string Word { get; set; }
    }
}
