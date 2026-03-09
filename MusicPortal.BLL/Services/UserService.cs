using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.BLL.Services
{
    public class UserService : IUserService<UserDTO>
    {
        IUnitOfWork Database { get; set; }
        
        public UserService(IUnitOfWork uow) 
        { 
            Database = uow;
        }

        public async Task<UserDTO> Get(int id)
        {
            var user = await Database.Users.Get(id);
            return user != null ? new UserDTO { Id = user.Id, Login = user.Login } : null;
        }

        public async Task Create(UserDTO userDTO)
        {
            string salt = GenerateSalt();

            string hashedPassword = HashPassword(userDTO.Password, salt);

            var user = new User
            {
                Login = userDTO.Login,
                Password = hashedPassword,
                Salt = salt
            };

            await Database.Users.Create(user);
            await Database.Save();
        }

        public async Task<UserDTO?> Login(UserDTO userDTO)
        {
            var user = await Database.Users.Get(userDTO.Login);
            if (user == null)
            {
                return null; 
            }

            var hashedPassword = HashPassword(userDTO.Password, user.Salt);

            if (user.Password == hashedPassword)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Login = user.Login
                };
            }

            return null;
        }
        private string GenerateSalt()
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            return Convert.ToHexString(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(salt + password);
            byte[] byteHash = MD5.HashData(bytes);
            return Convert.ToHexString(byteHash);
        }

        public async Task<bool> IsUnique(string login)
        {
            var users = await Database.Users.GetAll(); 
            return !users.Any(u => u.Login.ToLower() == login.ToLower());
        }
    }
}
