using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tdd_NerdStore.Core.Interfaces;

namespace Tdd_NerdStore.Data
{
    public class ApplicationContext : DbContext, IUnitOfWork
    {
        public readonly IMediator _mediator;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IMediator mediator) : base(options)
        {
            this._mediator = mediator;
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediator.PublishEvents(this);

            return success;
        }
    }
}
