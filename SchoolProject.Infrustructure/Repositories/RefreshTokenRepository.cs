using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> _userTokens;
        #endregion
        #region Ctor
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userTokens = dbContext.Set<UserRefreshToken>();
        }
        #endregion
        #region Methods
        #endregion

    }
}
