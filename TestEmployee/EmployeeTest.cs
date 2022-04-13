using CrudDemo.Controllers;
using CrudDemo.Data;
using CrudDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestEmployee
{
    public class EmployeeTest
    {

        private readonly Mock<IEmployeeRepository> service;

        public EmployeeTest()
        {
            service = new Mock<IEmployeeRepository>();
        }


        private List<Employee> GetSampleEmployee()
        {
            List<Employee> output = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Jhon",
                    Age = 10,
                    Email = "jhon@gmail.com",

                },
                new Employee
                {
                    Id = 2,
                    Name = "Jhon2",
                    Age = 10,
                    Email = "jhon2@gmail.com",
                },
                new Employee
                {
                    Id = 4,
                    Name = "Jhon4",
                    Age = 10,
                    Email = "jhon4@gmail.com",
                },

            };
            return output;
        }



        [Fact]
        public void GetEmployee_ListOfEmployees()
        {
            //arrange
            var employee = GetSampleEmployee();
            service.Setup(x => x.GetAllEmployees())
                .Returns(GetSampleEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployees();
            var result = actionResult as OkObjectResult;
            var actual = result.Value as IEnumerable<Employee>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleEmployee().Count(), actual.Count());
        }


        [Fact]
        public void GetEmployeeById_EmployeeObject_IdExists()
        {
            //arrange
            var employees = GetSampleEmployee();
            var firstEmployee = employees[0];
            service.Setup(x => x.GetEmployeeById((int)1))
                .Returns(firstEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployee((int)1);
            var result = actionResult as OkObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(result.Value, firstEmployee);

            //result.Value.Should().BeEquivalentTo(firstEmployee);
        }


        [Fact]
        public void GetEmployeeById_EmployeeObject_IdNotExists()
        {
            //arrange
            var employees = GetSampleEmployee();
            var firstEmployee = employees[0];
            service.Setup(x => x.GetEmployeeById((int)1))
                .Returns(firstEmployee);
            var controller = new EmployeeController(service.Object);

            //act
            var actionResult = controller.GetEmployee((int)3);
            var result = actionResult.ExecuteResultAsync;

            //assert
            Assert.IsType<OkObjectResult>(result);
            //Assert.Equal(result.Value, firstEmployee);

            //result.Value.Should().BeEquivalentTo(firstEmployee);
        }


        [Fact]
        public void AddEmployee_CreatedStatus_PassingEmployeeObject()
        {
            var employees = GetSampleEmployee();
            var newEmployee = employees[0];
            var controller = new EmployeeController(service.Object);
            var actionResult = controller.AddEmployee(newEmployee);
            var result = actionResult.ExecuteResultAsync;
            Assert.IsType<CreatedAtRouteResult>(result);

        }


    }
}