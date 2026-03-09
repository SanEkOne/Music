using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.Services
{
    public class MusicService : IEntityService<MusicDTO>
    {
        IUnitOfWork Database { get; set; }

        public MusicService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task Create(MusicDTO musicDTO)
        {
            var music = new Music
            {
                Id = musicDTO.Id,
                Name = musicDTO.Name,
                Author = musicDTO.Author,
                Genre = musicDTO.Genre,
                AudioData = musicDTO.AudioData
            };

            await Database.Musics.Create(music);
            await Database.Save();
        }

        public async Task Update(MusicDTO musicDTO)
        {
            //var music = new Music
            //{
            //    Id = musicDTO.Id,
            //    Name = musicDTO.Name,
            //    Author = musicDTO.Author,
            //    Genre = musicDTO.Genre,
            //    AudioData = musicDTO.AudioData
            //};

            //Database.Musics.Update(music);
            //await Database.Save();

            var music = await Database.Musics.Get(musicDTO.Id);

            if (music != null)
            {
                music.Name = musicDTO.Name;
                music.Author = musicDTO.Author;
                music.Genre = musicDTO.Genre;

                if (musicDTO.AudioData != null && musicDTO.AudioData.Length > 0)
                {
                    music.AudioData = musicDTO.AudioData;
                }

                Database.Musics.Update(music);
                await Database.Save();
            }
        }

        public async Task Delete(int id)
        {
            await Database.Musics.Delete(id);
            await Database.Save();
        }

        public async Task<MusicDTO> Get(int id)
        {
            var music = await Database.Musics.Get(id);
            if (music == null)
                throw new ValidationException("Не корректный ввод!");

            return new MusicDTO
            {
                Id = music.Id,
                Name = music.Name,
                Author = music.Author,
                Genre = music.Genre,
                AudioData = music.AudioData
            };
        }

        public async Task<IEnumerable<MusicDTO>> GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Music, MusicDTO>(), NullLoggerFactory.Instance);

            return config.CreateMapper().Map<IEnumerable<MusicDTO>>(await Database.Musics.GetAll());
        }
    }
}
