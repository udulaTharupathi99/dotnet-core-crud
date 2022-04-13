using CrudDemo.Model;

namespace CrudDemo.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly EmployeeContext context;

        public EmployeeRepository(EmployeeContext context)
        {
            this.context = context;

        }


        public List<Employee> GetAllEmployees()
        {
            return context.Employee.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = context.Employee.Find(id);
            return employee;
        }



        public Employee AddEmployee(Employee employee)
        {
            //employee.Id = context.Employee.Id;
            context.Employee.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {   
            context.Employee.Remove(employee);
            context.SaveChanges();
        }

        public Employee EditEmployee(Employee employee)
        {
            var oldEmp = context.Employee.Find(employee.Id);
            if (oldEmp != employee)
            {
                
                 oldEmp.Name = employee.Name;
                 oldEmp.Age = employee.Age;
                 oldEmp.Email = employee.Email;
                
                context.Employee.Update(oldEmp);
                context.SaveChanges();
            }
            return employee;
        }
    }
}
