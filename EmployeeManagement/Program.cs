using System;
using EmployeeManagement.Utilities;

namespace EmployeeManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            HelperMethods.DisplayMessage("Please enter organization xml file path:");

            try
            {
                var fileName = Console.ReadLine();
                HelperMethods.DisplayMessage($"=====Reading {fileName}=====");

                using var handler = new OrganizationManagement();

                handler.LoadFileAndPrintDetails(fileName);

                HelperMethods.DisplayMessage("======Move Employees from one unit to another======");
                HelperMethods.DisplayMessage("Enter name of Unit 1:");
                var unitName1 = Console.ReadLine();

                HelperMethods.DisplayMessage("Enter name of Unit 2:");
                var unitName2 = Console.ReadLine();

                handler.SwapEmployee(unitName1, unitName2);

                Console.Read();

                handler.DisplayJson();
                           
            }
            catch(Exception ex)
            {
                Logger.Instance.Log("Oops! Something went wrong");
                Logger.Instance.Log("==========Exception Details===========");
                Logger.Instance.Log($"{ex.Message} : {ex.StackTrace}");
            }
            Console.Read();
        }
    }
}
