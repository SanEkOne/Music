using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.EF;

namespace MusicPortal.DAL.Repositories
{
    public class MusicRepository : IRepository<Music>
    {
        private Context db; 

        public MusicRepository(Context context)
        {
            this.db = context;
        } 

        public async Task<IEnumerable<Music>> GetAll()
        {
            return await db.Musics.ToListAsync();
        }

        public async Task<Music> Get(int id)
        {
            Music? music = await db.Musics.FindAsync(id);
            return music;
        }

        public async Task<Music?> Get(string name)
        {
            var musics = await db.Musics.Where(a => a.Name == name).ToListAsync();
            Music? music = musics.FirstOrDefault();
            return music;
        }

        public async Task Create(Music music)
        {
            await db.Musics.AddAsync(music);
        }

        public void Update(Music music)
        {
            db.Entry(music).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Music? music = await db.Musics.FindAsync(id);
            if (music != null)
                db.Musics.Remove(music);
        }
    }
}
