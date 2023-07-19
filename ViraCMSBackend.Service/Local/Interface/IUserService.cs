using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        void EditUser(User user);
        bool ExistUserByRoleId(int RoleId);
        Tuple<bool, bool> CheckUserNameAndEmailExist(string email, string username);
        int AddUser(User user);
        User LoginUser(string username, string password);
        Token GenToken(User user);
        List<User> GetAllUsersExceptCurrent(int userId);
        User GetUserByUsername(string username);
    }
}
