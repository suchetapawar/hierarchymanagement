using System;
using System.IO;
using System.Xml;

namespace EmployeeManagement.Utilities
{
    /// <summary>
    /// This class handles methods those are being used frequently.
    /// </summary>
    public static class HelperMethods
    {
        /// <summary>
        /// Reads xml file and returns the childnodes.
        /// </summary>
        /// <param name="fileName">Name of the xml file.</param>
        /// <returns></returns>
        public static (string, XmlNodeList) ReadXml(string fileName)
        {
            var doc = new XmlDocument();

            XmlNodeList units = null;
            var name = "";
            if (File.Exists(fileName))
            {
                doc.Load(fileName);

                var root = doc.DocumentElement;

                name = root.Attributes["Name"].InnerText;
                units = root.ChildNodes;

            }
            return (name,units);
        }

        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <param name="input">input string.</param>
        /// <returns>Returns true if input string has data else returns false.</returns>
        public static bool ValidateInput(string input)
        {
            var status = true;

            if(string.IsNullOrEmpty(input))
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Displays the message onto the screen. This can be updated as per our requirement.
        /// </summary>
        /// <param name="message">Message to be displayed to user.</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}