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

        public Employee(string name, DateTime hireDate, int workHours)
        {
            Id = 1;
            Name = name;
            HireDate = hireDate;
            WorkHours = workHours;
        }

        public override bool Equals(object obj)
        {
            if (obj is Employee employee)
            {
                return Id == employee.Id &&
                    Name == employee.Name &&
                    HireDate == employee.HireDate &&
                    WorkHours == employee.WorkHours;
            }

            return this == obj;
        }

        public override int GetHashCode()
        {
            return $"{Id}-{Name}-{HireDate}-{WorkHours}".GetHashCode();
        }
    }
}