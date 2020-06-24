using System;
using System.Collections.Generic;
using System.Linq;
using Siji_BookWebApi.Entities;
using System.Threading.Tasks;

namespace Siji_BookWebApi.Interface
{
    public interface IBook
    {
        void Add(Book book);
        Task<bool> AddAsync(Book book);
        Task<bool> Update(Book book);
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
