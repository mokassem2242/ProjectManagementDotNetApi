using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository: GenericRepository<MainProject>, IProjectRepository
    {
        public ProjectRepository(ProjectManagementDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public ProjectManagementDbContext _dbContext { get; }

        public async Task<List<MainProject>> ListAllWithTasksAsync()
        {
         return await   _dbContext.Projects.Include(x=>x.MainTasks).ToListAsync();
        }
    }
}
