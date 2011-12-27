using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChatServiceContract
{
    [DataContract]
    public class RegisterRequest
    {
        [DataMember]
        public string PartyName { get; set; }
    }
}
