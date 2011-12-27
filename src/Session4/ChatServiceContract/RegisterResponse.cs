using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChatServiceContract
{
    [DataContract]
    public class RegisterResponse
    {
        [DataMember]
        public bool Success { get; set; }
    }
}
