using Microsoft.Data.SqlClient;

namespace InternshipForm
{
    public static class EmployeeData
    {
        public static List<Employee> GiveMeData()
        {
            List<Employee> employeeList = new List<Employee>();

            
            employeeList.Add(new Employee
            {
                ID = 1,
                Name = "Ram",
                Position = "Software Engineer",
                Age = 30,
                Office = "Kathmandu"
            });

            employeeList.Add(new Employee
            {
              ID= 2,
                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID = 3,
                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });

            employeeList.Add(new Employee
            {
                ID =4,
                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID = 5,
                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID =6,
                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID =7,
                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID =8,
                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });

            employeeList.Add(new Employee
            {
                ID =9,
                Name = "Shyam",
                Position = "Manager",
                Age = 25,
                Office="Bhaktpr",
            });
            employeeList.Add(new Employee
            {
                ID=10,
                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID=11,
                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID=12,
                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                ID=13,  
                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {
                  ID=14,  
                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });

            return employeeList;
        }
    }

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Office { get; set; }
    }



}
