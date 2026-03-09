using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.DAL.EF
{
    public class Context : DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminSalt = GenerateSalt();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Login = "Admin",
                Salt = adminSalt,
                Password = HashPassword("123admin", adminSalt)
            });

            modelBuilder.Entity<Music>().HasData(
                new Music
                {
                    Id = 1,
                    Name = "Test music",
                    Author = "Empty",
                    Genre = "Test",
                    AudioData = System.IO.File.ReadAllBytes("C:\\Users\\user\\Music\\music.m4a")
                }
            );
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
    }
}
