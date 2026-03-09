using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPortal.BLL.Interfaces
{
    public interface IEntityService<TDto> where TDto : class
    {
        Task Create(TDto dto);
        Task Update(TDto dto);
        Task Delete(int id);
        Task<TDto> Get(int id);
        Task<IEnumerable<TDto>> GetAll();
    }
}
