using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FirstServiceConsole
{
    [DataContract]
    class WordLookupReq
    {
        [DataMember]
        public string Word { get; set; }
    }
}
