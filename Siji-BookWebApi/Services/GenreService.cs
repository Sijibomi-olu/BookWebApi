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
    public class GenreService : IGenre
    {
        private BookApiDataContext _context;
        public GenreService(BookApiDataContext context)
        {
            _context = context;
        }

        public void Add(Genre genre)
        {
            _context.Add(genre);
            _context.SaveChanges();
        }
        public async Task<bool> AddAsync(Genre genre)
        {
            try
            {
                await _context.AddAsync(genre);
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
            var genre = await _context.Genres.FindAsync(Id);

            if (genre != null)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {

            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(int Id)
        {
            var genre = await _context.Genres.FindAsync(Id);

            return genre;
        }

        public async Task<bool> Update(Genre genre)
        {
            var gen = await _context.Genres.FindAsync(genre.Id);
            if (gen != null)
            {
                gen.Name = genre.Name;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }
    }
}
