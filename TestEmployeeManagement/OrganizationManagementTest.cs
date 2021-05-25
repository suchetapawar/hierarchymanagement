using Xunit;
using EmployeeManagement;
using EmployeeManagement.Models;

namespace TestEmployeeManagement
{
    public class OrganizationManagementTest
    {
        [Fact]
        public void LoadFileAndPrintDetailsTest()
        {
            using var manager = new OrganizationManagement();

            var fileName = "../../../../Organization.xml";
            manager.LoadFileAndPrintDetails(fileName);

            Assert.NotNull(manager.Org);

            Assert.Equal("ABC Inc.", manager.Org.Name);

            Assert.Equal(3, manager.Org.SubUnits.Count);

            Assert.Equal(3, manager.Org.Employees.Count);
        }

        [Fact]
        public void SwapEmployeesTest()
        {
            using var manager = new OrganizationManagement();

            manager.Org = new Organization("TestOrg");

            var unit1 = new Unit("Unit1") { Parent = manager.Org };
            var emp1 = new Employee("emp1", "Title 1") { Parent = unit1 };
            var emp2 = new Employee("emp2", "Title 2") { Parent = unit1 };
            var emp3 = new Employee("emp3", "Title 3") { Parent = unit1 };

            unit1.AddNode(emp1);
            unit1.AddNode(emp2);
            unit1.AddNode(emp3);

            var unit2 = new Unit("Unit2") { Parent = manager.Org };
            var emp4 = new Employee("emp4", "Title 4") { Parent = unit2 };
            var emp5 = new Employee("emp5", "Title 5") { Parent = unit2 };

            unit2.AddNode(emp4);
            unit2.AddNode(emp5);

            manager.Org.AddNode(unit1);
            manager.Org.AddNode(unit2);

            manager.SwapEmployee("Unit1", "Unit2");
            Assert.Collection(unit1.Employees,
                item =>
                {
                    Assert.Equal(emp4.Name, item.Name);
                    Assert.Equal(emp4.Title, item.Title);
                },
                item =>
                {
                    Assert.Equal(emp5.Name, item.Name);
                    Assert.Equal(emp5.Title, item.Title);
                });
        }
    }
}
