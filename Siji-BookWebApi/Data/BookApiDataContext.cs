using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siji_BookWebApi.Entities;
using Siji_BookWebApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace Siji_BookWebApi.Data
{
    public class BookApiDataContext : DbContext
    {
        public BookApiDataContext(DbContextOptions<BookApiDataContext> options) : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Characteristics> Characteristics { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
