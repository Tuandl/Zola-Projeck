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

        public NodeRelationship()
        {
            Friends = new HashSet<NodeRelationship>(new NodeRelationShipEqualityComparer());
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
}
