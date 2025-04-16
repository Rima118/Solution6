using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskProject.Common.TaskProjectModel;

namespace TaskProject.DAL
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EntityCourse2");
        }

        public DbSet<TaskProjectModel> TaskProjectModels { get; set; }
    }
}

