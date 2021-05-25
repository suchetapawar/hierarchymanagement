using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Utilities;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// This class represents an Employee in the organization.
    /// </summary>
    public class Employee : IEmployee, IDisposable
    {
        /// <summary>
        /// Title of the employee.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Reference to the unit which this employee belongs to.
        /// </summary>
        [JsonIgnore]
        public Hierarchy Parent { get; set; }

        /// <summary>
        /// Name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        public bool IsDisposed;

        readonly GCHandle stackHandle;

        /// <summary>
        /// Constructor of the Employee class.
        /// </summary>
        /// <param name="name">Name of the employee.</param>
        /// <param name="title">Title of the employee.</param>
        public Employee(string name, string title)
        {
            Name = name;
            Title = title;
        }

        /// <inheritdoc></inheritdoc>
        public void PrintDetails()
        {
            string format = "{0,-20} {1,-20} {2,-20}";
            string[] row = new string[] { Name, Title, Parent.Name };
            HelperMethods.DisplayMessage(string.Format(format, row));
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                if (stackHandle.IsAllocated)
                {
                    stackHandle.Free();
                }
            }
        }
    }
}