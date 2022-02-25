using MediatR;
using System.Linq;
using System.Threading.Tasks;
using Tdd_NerdStore.Core.DomainObjects;

namespace Tdd_NerdStore.Data
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents(this IMediator mediator, ApplicationContext context)
        {
            var domainEntities = context.ChangeTracker
                       .Entries<Entity>()
                       .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                       .SelectMany(x => x.Entity.Notifications)
                       .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                       .Select(async (domainEvent) =>
                       {
                           await mediator.Publish(domainEvent);
                       });

            await Task.WhenAll(tasks);
        }
    }
}
