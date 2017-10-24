using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ServerLibrary
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public bool IsMale { get; set; }
        [DataMember]
        public bool IsOnline { get; set; }
    }

    public class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (x.Username == y.Username) return true;
            return false;
        }

        public int GetHashCode(User obj)
        {
            return obj.Username.GetHashCode();
        }
    }

    [DataContract]
    public class DataFile
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public byte[] Data { get; set; }
    }
}
