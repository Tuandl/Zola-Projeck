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
        void FriendOffline(User offlineFriend);

        [OperationContract(IsOneWay = true)]
        void FriendOnlineListChangeUnexpectedly();

        [OperationContract(IsOneWay = false)]
        bool ReceiveMessage(DataMessage message);

        [OperationContract(IsOneWay = true)]
        void FriendChangeAvatar(User friend);

        [OperationContract(IsOneWay = true)]
        void FriendIsWrittingMessage(User Friend);

        [OperationContract(IsOneWay = true)]
        void FriendStopWrittingMessage(User Friend);

        [OperationContract(IsOneWay = true)]
        void ReceiveMakeFriendRequest(User stranger);

        [OperationContract(IsOneWay =  true)]
        void GotANewFriend(User newFriend);

        [OperationContract(IsOneWay = true)]
        void SentMakeFriendRequest(User stranger);
    }
}
