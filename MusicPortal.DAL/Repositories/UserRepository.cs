using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.EF;

namespace MusicPortal.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private Context db;
        public UserRepository(Context context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            User? user = await db.Users.FindAsync(id);
            return user;
        }

        public async Task<User?> Get(string login)
        {
            var users = await db.Users.Where(a => a.Login == login).ToListAsync();
            User? user = users.FirstOrDefault();
            return user;
        }
        public async Task Create(User user)
        {
            await db.Users.AddAsync(user);
        }
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            User? user = await db.Users.FindAsync(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
