using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServerLibrary
{
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void FriendOnline(User onlineFriend);

        [OperationContract(IsOneWay = true)]
        void FriendOnlineListChangeUnexpectedly();
    }
}
