using Demo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Employee : Person
    {
        public double Salary { get; private set; }
        public ProfessionalLevel ProfessionalLevel { get; private set; }
        public IList<string> Skills { get; private set; }

        public Employee(string name, double salary)
        {
            Name = string.IsNullOrEmpty(name) ? "Fulano" : name;
            DefineSalary(salary);
            DefineSkills();
        }
        private void DefineSalary(double salary)
        {
            if (salary < 500) throw new Exception("Salary below allowed!");

            this.Salary = salary;
            if (salary < 200) ProfessionalLevel = ProfessionalLevel.Junior;
            else if (salary >= 2000 && salary < 8000) ProfessionalLevel = ProfessionalLevel.Full;
            else if (salary >= 8000) ProfessionalLevel = ProfessionalLevel.Senior;
        }

        private void DefineSkills()
        {
            var basicSkills = new List<string>()
            {
                "Programming Logic",
                "OOP"
            };

            this.Skills = basicSkills;

            switch (ProfessionalLevel)
            {
                case ProfessionalLevel.Full:
                    this.Skills.Add("Tests");
                    break;

                case ProfessionalLevel.Senior:
                    this.Skills.Add("Tests");
                    this.Skills.Add("MicroServices");
                    break;
            }
        }
    }
}
