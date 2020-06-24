using System;
using System.Collections.Generic;
using System.Linq;
using Siji_BookWebApi.Entities;
using System.Threading.Tasks;

namespace Siji_BookWebApi.Interface
{
    public interface ICategory
    {
        void Add(Category category);
        Task<bool> AddAsync(Category category);
        Task<bool> Update(Category category);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
