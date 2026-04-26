using System;
using System.Linq;
using Test2.models.Entities;
using Test2.Models.Data;

namespace Test2.models.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User GetByUsername(string username)
        {
            try
            {
                return _context.Users
                               .FirstOrDefault(u => u.Username == username);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve user.", ex);
            }
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public void Add(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add user.", ex);
            }
        }
    }
}