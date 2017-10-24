using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    class NodeRelationship
    {
        public User User { get; }
        public HashSet<NodeRelationship> Friends { get; }
        public HashSet<NodeRelationship> SentPendingRequest { get; }
        public HashSet<NodeRelationship> ReceivedPendingRequest { get; }

        public NodeRelationship(User curUser)
        {
            this.User = curUser;
            Friends = new HashSet<NodeRelationship>(new NodeRelationShipEqualityComparer());
            SentPendingRequest = new HashSet<NodeRelationship>(new NodeRelationShipEqualityComparer());
            ReceivedPendingRequest = new HashSet<NodeRelationship>(new NodeRelationShipEqualityComparer());
        }
    }

    class NodeRelationShipEqualityComparer : IEqualityComparer<NodeRelationship>
    {
        public bool Equals(NodeRelationship x, NodeRelationship y)
        {
            return x.User.Username == y.User.Username;
        }

        public int GetHashCode(NodeRelationship obj)
        {
            return obj.User.Username.GetHashCode();
        }
    }

    public enum RelationshipType
    {
        Friend = 0, Pending = 1
    }
}
