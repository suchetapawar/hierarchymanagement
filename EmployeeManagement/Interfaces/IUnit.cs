using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    /// <summary>
    /// Interface which consolidates methods for unit.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// This method should add employee to the list of employees under the unit.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        EmployeeList AddNode(Employee employee);

        /// <summary>
        /// This method should add unit to the list of units under the unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        UnitList AddNode(Unit unit);
    }
}