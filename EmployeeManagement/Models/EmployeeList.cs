using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// List of the employees.
    /// </summary>
    public class EmployeeList : List<Employee>
    {
        /// <summary>
        /// Reference to the unit which this employee belongs to.
        /// </summary>
        public Unit Parent { get; set; }

        /// <summary>
        /// Adds an employee to the list.
        /// </summary>
        /// <param name="employee"></param>
        internal void AddNode(Employee employee)
        {
            base.Add(employee);
        }
    }
}