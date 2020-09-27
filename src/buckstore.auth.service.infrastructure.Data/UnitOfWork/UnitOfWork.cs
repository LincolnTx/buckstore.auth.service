using System.Threading.Tasks;
using buckstore.auth.service.infrastructure.Data.Context;
using buckstore.auth.service.domain.SeedWork;

namespace buckstore.auth.service.infrastructure.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public UnitOfWork(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<bool> Commit()
		{
			return await _applicationDbContext.SaveEntitiesAsync();
		}

		public void Dispose()
		{
			_applicationDbContext.Dispose();
		}
	}
}