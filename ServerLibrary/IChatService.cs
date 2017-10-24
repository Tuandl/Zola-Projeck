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
        List<User> FindPeople(string username);
    }
}
