using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.Database;
using final.Models;
using Microsoft.EntityFrameworkCore;

namespace final.Services
{
    class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool isSuccess, string message, User? user)> RegisterAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                return (false, "Пользователь с таким логином уже существует", null);

            var user = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, String.Empty, user);
        }

        public async Task<(bool isSuccess, string message, User? user)> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return (false, "Пользователь не найден", null);

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return (false, "Неверный пароль", null);

            return (true, String.Empty, user);
        }
    }
}
