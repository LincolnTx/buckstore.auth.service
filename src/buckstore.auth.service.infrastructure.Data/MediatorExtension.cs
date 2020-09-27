using System.Threading.Tasks;
using buckstore.auth.service.infrastructure.Data.Context;
using MediatR;
using System.Linq;
using buckstore.auth.service.domain.SeedWork;

namespace buckstore.auth.service.infrastructure.Data
{
	static class MediatorExtension
	{
		public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext ctx)
		{
			var domainEntities = ctx.ChangeTracker
				.Entries<Entity>()
				.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

			var domainEvents = domainEntities
				.SelectMany(x => x.Entity.DomainEvents)
				.ToList();

			domainEntities.ToList()
				.ForEach(entity => entity.Entity.ClearDomainEvent());

			foreach (var domainEvent in domainEvents)
			{
				await mediator.Publish(domainEvent);
			}
		}
	}
}
