using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServerLibrary
{
    [ServiceContract(SessionMode = SessionMode.Required,
        CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        bool Register(string username, string password, string name);

        [OperationContract(IsOneWay = false, IsInitiating = true)]
        bool Login(string username, string password);

        [OperationContract(IsOneWay = false)]
        List<User> GetFriends(string username);

        [OperationContract(IsOneWay = false)]
        DataFile GetAvatarFile(string username);

        [OperationContract(IsOneWay = false)]
        bool IsUserHasAvatar(string username);

        [OperationContract(IsOneWay = false)]
        List<User> FindStranger(string curUsername, string strangerUsername);

        [OperationContract(IsOneWay = true)]
        void SendMessage(DataMessage message);

        [OperationContract(IsOneWay = false)]
        List<DataMessage> GetUnreceivedMessages(string username);

        [OperationContract(IsOneWay = false)]
        bool UpdateInformation(User user);

        [OperationContract(IsOneWay = false)]
        bool UpdatePassword(string username, string password);

        [OperationContract(IsOneWay = false)]
        bool UpdateAvatar(string username, DataFile newAvatar);

        [OperationContract(IsOneWay = true)]
        void Writting(User writer, User waiter);

        [OperationContract(IsOneWay = true)]
        void SendFriendRequest(string sender, string stranger);

        [OperationContract(IsOneWay = false)]
        List<User> GetPendingFriendRequests(User user);

        [OperationContract(IsOneWay = true)]
        void AcceptFriendRequest(User foo, User bar);

        [OperationContract(IsOneWay = false)]
        List<User> GetSentFriendRequest(User user);
    }
}
