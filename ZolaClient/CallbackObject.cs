using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZolaClient.ZolaService;
using System.ServiceModel;

namespace ZolaClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CallbackObject : ZolaService.IChatServiceCallback
    {
        #region Implement callback
        public void FriendChangeAvatar(User friend)
        {
            throw new NotImplementedException();
        }

        public void FriendIsWrittingMessage(User Friend)
        {
            throw new NotImplementedException();
        }

        public void FriendOnline(User onlineFriend)
        {
            throw new NotImplementedException();
        }

        public void FriendOnlineListChangeUnexpectedly()
        {
            throw new NotImplementedException();
        }

        public void GotANewFriend()
        {
            throw new NotImplementedException();
        }

        public void ReceiveMakeFriendRequest(User stranger)
        {
            throw new NotImplementedException();
        }

        public bool ReceiveMessage(DataMessage message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Async methods
        public IAsyncResult BeginFriendChangeAvatar(User friend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginFriendIsWrittingMessage(User Friend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginFriendOnline(User onlineFriend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginFriendOnlineListChangeUnexpectedly(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGotANewFriend(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceiveMakeFriendRequest(User stranger, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceiveMessage(DataMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendChangeAvatar(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void EndFriendIsWrittingMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void EndFriendOnline(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void EndFriendOnlineListChangeUnexpectedly(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void EndGotANewFriend(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void EndReceiveMakeFriendRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool EndReceiveMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void FriendOffline(User offlineFriend)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginFriendOffline(User offlineFriend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendOffline(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
