using Features.Core;
using System;

namespace Features.Clients
{
    public class Client : Entity
    {
        public string Name { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public DateTime RegisterDate { get; protected set; }
        public string Email { get; protected set; }
        public bool Active { get; protected set; }

        protected Client()
        {

        }

        public Client(Guid id, string name, string lastName, DateTime birthDate, DateTime registerDate, string email, bool active)
        {
            this.Id = id;
            this.Name = name;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.RegisterDate = registerDate;
            this.Email = email;
            this.Active = active;
        }

        public string FullName()
        {
            return $"{this.Name} {this.LastName}";
        }

        public bool IsSpecial()
        {
            return this.RegisterDate <= DateTime.Now.AddYears(-3) && this.Active;
        }

        public void InactivateOrActivate()
        {
            this.Active = !this.Active;
        }

        public override bool IsValid()
        {
            ValidationResult = new ClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
