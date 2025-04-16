using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Common.TaskProjectModel;

namespace TaskProject.DAL.Interface
{
    public interface ITaskRepository
    {
        Task Create(TaskProjectModel taskModel);
        Task<List<TaskProjectModel>> GetAllTasks();
        Task Delete(int id);
        Task<string> Update(int id, TaskProjectModel aaa);
    }
}
