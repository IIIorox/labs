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
    class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserTask>> GetTasksAsync()
        {
            if (!UserSession.IsAuthenticated)
                throw new InvalidOperationException("Пользователь не авторизован");

            return await _context.Tasks
                .Where(t => t.UserId == UserSession.CurrentUser.Id)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<UserTask> CreateTaskAsync(string title, string description)
        {
            if (!UserSession.IsAuthenticated)
                throw new InvalidOperationException("Пользователь не авторизован");

            var task = new UserTask
            {
                Title = title,
                Description = description,
                CreatedAt = DateTime.Now,
                UserId = UserSession.CurrentUser.Id
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task UpdateTaskAsync(UserTask task)
        {
            if (!UserSession.IsAuthenticated)
                throw new InvalidOperationException("Пользователь не авторизован");

            var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id && t.UserId == UserSession.CurrentUser.Id);

            if (existing == null)
                throw new InvalidOperationException("Задача не найдена или не принадлежит пользователю");

            existing.Title = task.Title;
            existing.Description = task.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(UserTask task)
        {
            if (!UserSession.IsAuthenticated)
                throw new InvalidOperationException("Пользователь не авторизован");

            var existing = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id && t.UserId == UserSession.CurrentUser.Id);

            if (existing != null)
            {
                _context.Tasks.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
