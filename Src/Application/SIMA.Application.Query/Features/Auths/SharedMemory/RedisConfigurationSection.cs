using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.Text;

namespace SIMA.Application.Query.Features.Auths.SharedMemory
{
    public sealed class RedisConfigurationSection
    {
        [DataMember(Name = "Host")]
        public string Host { set; get; }

        [DataMember(Name = "Port")]
        public int Port { set; get; }

        [DataMember(Name = "Password")]
        public string Password { set; get; }

        [DataMember(Name = "DatabaseID")]
        public long DatabaseID { set; get; }


    }
}

