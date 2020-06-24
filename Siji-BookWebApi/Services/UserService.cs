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
    public class UserService : IUser
    {
        private BookApiDataContext _context;

        public UserService(BookApiDataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username || x.Email == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username || x.Email == user.Email))
                throw new Exception("Username \"" + user.Username + "\" or Email \"" + user.Email + " is already taken");

            // passwordHash, passwordSalt;
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }


        public async Task<bool> Update(User user, string password = null)
        {
            var _user = await _context.Users.FindAsync(user.Id);
            if (_user != null)
            {
                _user.Username = user.Username;
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;

        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


        public async Task<bool> Delete(int Id)
        {
            // find the entity/object
            var user = await _context.Users.FindAsync(Id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<User>> GetAll()
        {

            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int Id)
        {
            var user = await _context.Users.FindAsync(Id);

            return user;
        }

    }
}
