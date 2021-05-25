using System.Text.Json.Serialization;
namespace EmployeeManagement.Models
{
    /// <summary>
    /// This class represents each unit in the organization.
    /// </summary>
    public class Unit : Hierarchy
    {
        /// <summary>
        /// Constructor of the class Unit.
        /// </summary>
        /// <param name="name"></param>
        public Unit(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Reference to the unit which this employee belongs to.
        /// </summary>
        [JsonIgnore] public Hierarchy Parent { get; set; }

        /// <inheritdoc/>
        public override UnitList AddNode(Unit unit)
        {
            SubUnits ??= new UnitList();
            SubUnits.AddNode(unit);
            return SubUnits;
        }

        /// <inheritdoc/>
        public override EmployeeList AddNode(Employee employee)
        {
            Employees ??= new EmployeeList();
            Employees.AddNode(employee);
            return Employees;
        }

        /// <summary>
        /// This method swaps the employee of current unit with the unit in the parameters.
        /// </summary>
        /// <param name="anotherUnit">Another unit to swap the employees with.</param>
        public void SwapEmployee(Unit anotherUnit)
        {
            var thisEmployees = Employees;
            Employees = anotherUnit.Employees;

            foreach(var emp in Employees)
            {
                emp.Parent = this;
            }
            anotherUnit.Employees = thisEmployees;

            foreach (var emp in anotherUnit.Employees)
            {
                emp.Parent = anotherUnit;
            }
        }
    }
}
