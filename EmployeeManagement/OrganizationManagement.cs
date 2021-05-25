using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using EmployeeManagement.Models;
using EmployeeManagement.Utilities;

namespace EmployeeManagement
{
    /// <summary>
    /// This class is a wrapper for all organization level activities.
    /// </summary>
    public class OrganizationManagement : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Represents the hierarchy in the whole Organization.
        /// </summary>
        public Organization Org { get; set; }

        Dictionary<string, Unit> cache = new Dictionary<string, Unit>();

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public OrganizationManagement()
        {
        }

        /// <summary>
        /// This method loads a file and print the structure.
        /// </summary>
        /// <param name="fileName">Name of the xml file.</param>
        public void LoadFileAndPrintDetails(string fileName)
        {
            if (HelperMethods.ValidateInput(fileName))
            {
                HelperMethods.DisplayMessage("Sucessfully loaded xml file.");

                var (parentName,units) = HelperMethods.ReadXml(fileName);

                if (units != null)
                {
                    Org = new(parentName);

                    HelperMethods.DisplayMessage("=====Building Organization Chart=====");

                    Org.ParseXml(Org, units, cache);


                    HelperMethods.DisplayMessage("======Organization Chart======");

                    Org.PrintOrgDetails();
                    Console.Read();
                }
                else
                {
                    HelperMethods.DisplayMessage("Error while parsing given file.");
                    Logger.Instance.Log("Incorrect xml");
                }
            }
            else
            {
                HelperMethods.DisplayMessage("Invalid Input");
                Logger.Instance.Log("Incorrect file name");
            }
        }

        /// <summary>
        /// This method swaps the employee between two units.
        /// </summary>
        /// <param name="unitName1">Unit 1</param>
        /// <param name="unitName2">Unit 2</param>
        public void SwapEmployee(string unitName1, string unitName2)
        {
            if (HelperMethods.ValidateInput(unitName1))
            {
                if (HelperMethods.ValidateInput(unitName2))
                {
                    var unit1 = cache[unitName1.ToUpper()];
                    var unit2 = cache[unitName2.ToUpper()];
                    Organization.SwapEmployee(unit1, unit2);
                    HelperMethods.DisplayMessage($"Successfully swapped employees between \"{unitName1}\" and \"{ unitName2 }\"");
                }
                else
                {
                    HelperMethods.DisplayMessage("Input entered is not right.");
                    HelperMethods.DisplayMessage("No updates to Organization Chart");
                    Logger.Instance.Log("Invalid Input");
                }
            }
            else
            {
                HelperMethods.DisplayMessage("Input entered is not right.");
                HelperMethods.DisplayMessage("No updates to Organization Chart");
                Logger.Instance.Log("Invalid Input");
            }
            HelperMethods.DisplayMessage("======Updated Organization Chart======");
            Org.PrintOrgDetails();
        }

        /// <summary>
        /// This method displays the organization chart in json format.
        /// </summary>
        public void DisplayJson()
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(Org, options);

            HelperMethods.DisplayMessage("======Json Response======");

            HelperMethods.DisplayMessage(json);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Org.Dispose();
                }
                              
                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}