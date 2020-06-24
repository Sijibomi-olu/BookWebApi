using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siji_BookWebApi.Entities;

namespace Siji_BookWebApi.Interface
{
    public interface IGenre
    {
        void Add(Genre genre);
        Task<bool> AddAsync(Genre genre);
        Task<bool> Update(Genre genre);
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
