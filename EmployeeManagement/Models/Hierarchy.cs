using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Xml;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// This is the base class for all hierarchy eg. Organization/Unit.
    /// </summary>
    public abstract class Hierarchy : IDisposable
    {
        /// <summary>
        /// List of units under this Unit.
        /// </summary>
        [JsonPropertyName("Units")]
        public UnitList SubUnits { get; set; }

        /// <summary>
        /// List of employees under this unit.
        /// </summary>
        [JsonPropertyName("Employees")]
        public EmployeeList Employees { get; set; }

        /// <summary>
        /// This method adds new node in Employee list.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>returns list of employees.</returns>
        public abstract EmployeeList AddNode(Employee employee);

        /// <summary>
        /// This method adds new node in unit's list.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>returns list of units </returns>
        public abstract UnitList AddNode(Unit unit);

        /// <summary>
        /// Name of the Unit.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specifies if object has been disposed or not.
        /// </summary>
        public bool IsDisposed;

        readonly GCHandle stackHandle;

        /// <summary>
        /// Parse the xml and create the hierarchy.
        /// </summary>
        /// <param name="parent"> Parent node in the hierarchy.</param>
        /// <param name="xml"> Xml to be parsed to generate hierarchy.</param>
        /// <param name="cache"> Reference to the cache in order to save all the units to get the quick access.</param>
        /// <returns></returns>
        public Hierarchy ParseXml(Hierarchy parent, XmlNodeList xml, Dictionary<string, Unit> cache)
        {
            foreach (XmlNode node in xml)
            {
                switch (node.Name)
                {
                    case "Employee":
                        var employee = new Employee(node.InnerText, node.Attributes["Title"].InnerText) { Parent = parent };
                        parent.AddNode(employee);
                        break;
                    case "Unit":
                        var newUnit = new Unit(node.Attributes["Name"].InnerText);
                        ParseXml(newUnit, node.ChildNodes, cache);
                        cache.Add(newUnit.Name.ToUpper(), newUnit);
                        newUnit.Parent = parent;
                        parent.AddNode(newUnit);
                        break;
                    case "Units":
                        ParseXml(parent, node.ChildNodes, cache);
                        break;
                    default: throw new InvalidDataException();
                }
            }
            return parent;
        }

        /// <summary>
        /// This method prints the employee details and the employee under all the sub units in this unit.
        /// </summary>
        public void PrintDetails()
        {
            if (Employees!=null)
            {
                foreach (var emp in Employees)
                {
                    emp.PrintDetails();
                }
            }
            if (SubUnits!=null)
            {
                foreach (var unit in SubUnits)
                {
                    unit.PrintDetails();
                }
            }
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if(!IsDisposed)
            {
                IsDisposed = true;
                if (stackHandle.IsAllocated)
                {
                    stackHandle.Free();
                }
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);

            if (SubUnits != null)
            {
                foreach (var unit in SubUnits)
                {
                    unit.Dispose();
                }
            }

            if(Employees != null)
            {
                foreach (var emp in Employees)
                {
                    emp.Dispose();
                }
            }
            IsDisposed = true;

            GC.SuppressFinalize(this);
        }
    }
}