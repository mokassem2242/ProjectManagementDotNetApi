using ProjectManagementApp.Domain.Entities;

namespace ProjectManagementApp.Application.Contracts.Infrastructure.Persistence
{
    public interface IDomainUserRepository : IGenericRepository<DomainUser>
    {
        Task<DomainUser> GetByIdentityId (Guid identityId);
    }
} 