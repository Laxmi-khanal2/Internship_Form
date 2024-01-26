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
                Name = "Ram",
                Position = "Software Engineer",
                Age = 30,
                Office = "Kathmandu"
            });

            employeeList.Add(new Employee
            {
              
                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });

            employeeList.Add(new Employee
            {

                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Hari",
                Position = "Developer",
                Age = 35,
                Office = "Kathmandu"
            });

            employeeList.Add(new Employee
            {
                Name = "Shyam",
                Position = "Manager",
                Age = 25,
                Office="Bhaktpr",
            });
            employeeList.Add(new Employee
            {

                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

                Name = "Shyam",
                Position = "HR Manager",
                Age = 35,
                Office = "Kathmandu"
            });
            employeeList.Add(new Employee
            {

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
        public string ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Office { get; set; }
    }



}
