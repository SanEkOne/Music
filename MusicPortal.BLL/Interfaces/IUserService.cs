using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface IUserService<TDto> where TDto : class
    {
        Task<TDto> Get(int id);
        Task Create(TDto dto);
        Task<TDto> Login(TDto dto);

        Task<bool> IsUnique(string login);

    }
}
