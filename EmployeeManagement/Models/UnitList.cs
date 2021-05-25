using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// List of the units.
    /// </summary>
    public class UnitList: List<Unit>
    {
        /// <summary>
        /// Reference to the unit which this unit belongs to.
        /// </summary>
        public Unit Parent { get; set; }

        /// <summary>
        /// Adds an unit to the list.
        /// </summary>
        /// <param name="unit"></param>
        internal void AddNode(Unit unit)
        {
            base.Add(unit);
        }
    }
}