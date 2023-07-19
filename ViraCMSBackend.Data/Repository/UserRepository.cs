﻿using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;


namespace ViraCMSBackend.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public UserRepository(ViraCMS_DBContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void AddUser(User user)
        {
            Create(user);
            Save();
        }

        public void EditUser(User user)
        {
            Update(user);
            Save();
        }

        public bool ExistUserByRoleId(int RoleId)
        {
            return FindByCondition(w => w.RoleId == RoleId && w.IsDeleted == false).Any();
        }

        public List<User> GetAllUsers()
        {
            return FindByCondition(w => w.IsDeleted == false).ToList();
        }

        public List<User> GetAllUsersExceptCurrent(int userId)
        {
            return FindByCondition(w => w.UserId != userId && w.IsDeleted == false).ToList();
        }

        public User GetUserById(int userId)
        {
            return FindByCondition(w => w.UserId == userId && w.IsDeleted == false).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            var x = _repositoryContext.Users.Where(w => w.UserName == username && w.IsDeleted == false);

            return x.FirstOrDefault();
        }

        public User GetUserLogin(string username, string password)
        {
            return FindByCondition(w => w.UserName == username && w.PassWord == password && w.IsDeleted == false).FirstOrDefault();
        }

        public bool IsExistEmail(string email)
        {
            return FindByCondition(w => w.Email == email && w.IsDeleted == false).Any();
        }

        public bool IsExistUserName(string username)
        {
            return FindByCondition(w => w.UserName == username && w.IsDeleted == false).Any();
        }
    }

}
