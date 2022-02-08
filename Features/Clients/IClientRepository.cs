using Features.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Clients
{
    public interface IClientRepository : IRepository<Client>
    {
        public Client GetByEmail(string email);
    }
}
