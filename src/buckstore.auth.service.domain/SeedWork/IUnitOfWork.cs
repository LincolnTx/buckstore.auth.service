using System;
using System.Threading.Tasks;

namespace buckstore.auth.service.domain.SeedWork
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}