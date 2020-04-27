using System;

namespace Theorem.MockNet.Http.Tests
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }
        public int WorkHours { get; set; }

        public Employee()
        {
        }

        public Employee(string name, DateTime hireDate, int workHours) => (Id, Name, HireDate, WorkHours) = (1, name, hireDate, workHours);

        public override bool Equals(object obj)
        {
            if (obj is Employee employee)
            {
                return (Id, Name, HireDate, WorkHours) == (employee.Id, employee.Name, employee.HireDate, employee.WorkHours);
            }

            return this == obj;
        }

        public override int GetHashCode()
        {
            return $"{Id}-{Name}-{HireDate}-{WorkHours}".GetHashCode();
        }
    }
}
