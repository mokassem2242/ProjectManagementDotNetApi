using ProjectManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Contracts.Infrastructure.Persistence
{
    public interface IProjectRepository : IGenericRepository<MainProject>
    {
       Task<List<MainProject>> ListAllWithTasksAsync (); 
    }
}
