using System;
using System.Collections.Generic;
using System.Text;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Music> Musics { get; }
        Task Save();
    }
}
