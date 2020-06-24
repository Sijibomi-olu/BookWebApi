using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siji_BookWebApi.Data;
using Siji_BookWebApi.Entities;
using Siji_BookWebApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace Siji_BookWebApi.Services
{
    public class AuthorService : IAuthor
    {
        private BookApiDataContext _context;
        public AuthorService(BookApiDataContext context)
        {
            _context = context;
        }

        public void Add(Author author)
        {
            _context.Add(author);
            _context.SaveChanges();
        }
        public async Task<bool> AddAsync(Author author)
        {
            try
            {
                await _context.AddAsync(author);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Delete(int Id)
        {
            // find the entity/object
            var author = await _context.Authors.FindAsync(Id);

            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {

            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetById(int Id)
        {
            var author = await _context.Authors.FindAsync(Id);

            return author;
        }

        public async Task<bool> Update(Author author)
        {
            var aut = await _context.Authors.FindAsync(author.Id);
            if (aut != null)
            {
                aut.Name = author.Name;
                aut.Title = author.Title;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
