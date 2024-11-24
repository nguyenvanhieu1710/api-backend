using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthenticationWEBAPI.Models
{
    public class EmployeeBL
    {
        public List<Employee> GetEmployees()
        {
            // In Realtime you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we hardcoded the data
            List<Employee> empList = new List<Employee>();
            for (int i = 0; i < 10; i++)
            {
                if (i > 5)
                {
                    empList.Add(new Employee()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Dept = "IT",
                        Salary = 1000 + i,
                        Gender = "Male"
                    });
                }
                else
                {
                    empList.Add(new Employee()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Dept = "HR",
                        Salary = 1000 + i,
                        Gender = "Female"
                    });
                }
            }
            return empList;
        }
    }
}