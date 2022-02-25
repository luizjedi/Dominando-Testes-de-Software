using System;
using Tdd_NerdStore.Core.DomainObjects.DomainInterfaces;

namespace Tdd_NerdStore.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
