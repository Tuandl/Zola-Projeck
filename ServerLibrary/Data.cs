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
        public bool? IsMale { get; set; }
        [DataMember]
        public bool IsOnline { get; set; }

        public override bool Equals(object obj)
        {
            bool res = false;
            if(obj is User)
            {
                User tmp = obj as User;
                if (this.Username == tmp.Username)
                    res = true;
            }
            return res;
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }
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

    [DataContract]
    public class DataMessage
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public User Sender { get; set; }
        [DataMember]
        public User Receiver { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public DateTime SentTime { get; set; }
    }

    [DataContract]
    public enum DataMessageStatus
    {
        [EnumMember]
        Sent = 0,
        [EnumMember]
        Received = 1
    }
}
