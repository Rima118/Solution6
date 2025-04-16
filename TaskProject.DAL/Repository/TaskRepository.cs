using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Common.TaskProjectModel;
using TaskProject.DAL.Interface;

namespace TaskProject.DAL.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(TaskProjectModel taskModel)
        {
            var taskitem = new TaskProjectModel
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                IsCompleted = taskModel.IsCompleted,
                CreatedAt = DateTime.Now
            };
            _context.TaskProjectModels.Add(taskModel);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TaskProjectModel>> GetAllTasks()
        {
            return await _context.TaskProjectModels.ToListAsync();
        }


        public async Task Delete(int id)
        {
            var task = await _context.TaskProjectModels.FindAsync(id);
            if (task != null)
            {
                _context.TaskProjectModels.Remove(task);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<string> Update(int id, TaskProjectModel aaa)
        {
            var task = await _context.TaskProjectModels.FindAsync(id);
            if (task != null)
            {
                task.Title = aaa.Title;
                task.Description = aaa.Description;
                task.IsCompleted = aaa.IsCompleted;

                await _context.SaveChangesAsync();
                return "Done";
            }
            return "Try Again";
        }


    }
}
