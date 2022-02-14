using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Clients
{
    public interface IClientService : IDisposable
    {
        public IEnumerable<Client> GetAllActives();
        public void Add(Client client);
        public void Update(Client client);
        public void Remove(Client client);
        public void InactivateOrActivate(Client client);
    }
}
