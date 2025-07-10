using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Application.Contracts.Infrastructure.Persistence;
using ProjectManagementApp.Domain.Entities;

namespace ProjectManagementApp.Infrastructure.Persistence.Repositories
{
    public class DomainUserRepository : GenericRepository<DomainUser>, IDomainUserRepository
    {
        public DomainUserRepository(ProjectManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public ProjectManagementDbContext _dbContext { get; }

        public async Task<DomainUser> GetByIdentityId(Guid identityId)
        {
         return await  _dbContext.Users.FirstOrDefaultAsync(x => x.ApplicationUserId == identityId);
        }
    }
} 