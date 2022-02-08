using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Clients
{
    public class ClientEmailNotification : INotification
    {
        public string  Origin { get; private set; }
        public string  Destiny { get; private set; }
        public string  Subject { get; private set; }
        public string  Message { get; private set; }

        public ClientEmailNotification(string origin, string destiny, string subject, string message)
        {
            this.Origin = origin;
            this.Destiny = destiny;
            this.Subject = subject;
            this.Message = message;
        }
    }
}
