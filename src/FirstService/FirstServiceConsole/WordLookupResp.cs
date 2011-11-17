using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FirstServiceConsole
{
    [DataContract]
    class WordLookupResp
    {
        [DataMember]
        public bool Exists { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
