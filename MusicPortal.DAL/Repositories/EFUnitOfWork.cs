using System;
using System.Collections.Generic;
using System.Text;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private Context db;
        private MusicRepository musicRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(Context context)
        {
            db = context;
        }

        public IRepository<Music> Musics
        {
            get
            {
                if (musicRepository == null)
                    musicRepository = new MusicRepository(db);
                return musicRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }
        public async Task Save() 
        {
            await db.SaveChangesAsync();
        }
    }
}
