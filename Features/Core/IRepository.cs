using Features.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Core
{
    public interface IRepository<TEntity>
    {
        public IEnumerable<TEntity> GetAll();
        public void Add(TEntity client);
        public void Update(TEntity client);
        public void Remove(Guid clientId);
        public void Dispose();
    }
}
