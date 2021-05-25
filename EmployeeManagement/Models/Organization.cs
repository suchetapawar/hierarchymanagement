using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EmployeeManagement.Utilities;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// This class represents Organization.
    /// </summary>
    public class Organization : Hierarchy
    {
        /// <summary>
        /// Constructor of the class Organization.
        /// </summary>
        /// <param name="name"></param>
        public Organization(string name)
        {
            Name = name;
        }

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
        /// Prints the organization chart.
        /// </summary>
        public void PrintOrgDetails()
        {
            string format = "{0,-20} {1,-20} {2,-20}";
            string[] heading = new string[] { "Name", "Title", "Unit Name" };

            HelperMethods.DisplayMessage(string.Format(format, heading));

            HelperMethods.DisplayMessage("=============================================================");
            this.PrintDetails();
        }

        /// <summary>
        /// This method swaps the employees between two different units.
        /// </summary>
        /// <param name="unit1"></param>
        /// <param name="unit2"></param>
        public static void SwapEmployee(Unit unit1, Unit unit2)
        {
            unit1.SwapEmployee(unit2);
        }
    }
}