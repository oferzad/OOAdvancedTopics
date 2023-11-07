using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OOAdvancedTopics
{
    class InvalidMonthArgument : Exception
    {
        private int monthID;
        public InvalidMonthArgument(int monthID, string message) : base(message)
        {
            this.monthID = monthID;
        }
        public InvalidMonthArgument(int monthID, string message, Exception e) : base(message, e)
        {
            this.monthID = monthID;
        }
        public int GetMonthID()
        {
            return this.monthID;
        }
    }
    class ExceptionsTraining
    {
        //This example show how to catch a custom exception with inner exception
        static void SeventhTry()
        {
            while (true)
            {
                Console.WriteLine("Hi, Please type month number: ");
                int monthID = int.Parse(Console.ReadLine());
                try
                {
                    int yearPart = 12 * 100 / monthID;
                    Console.WriteLine($"{monthID} presents that  {yearPart}% of the year passed!");
                    string month = GetMonthByID3(monthID);
                    Console.WriteLine($"You chose {month}");
                }
                catch (InvalidMonthArgument e)
                {
                    Console.WriteLine($"Got Invalid Month Exception: {e.Message}");
                    Console.WriteLine($"The inner excepton message is: {e.InnerException.Message}");
                }
                catch (DivideByZeroException e)
                {
                    Console.WriteLine($"Tried to divide by zero!!: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    //This code will run in all cases (if exception was coaught or not)
                    Console.WriteLine("I am in the finnaly section");
                }
            }
        }

        //this example show how to throw an exception while keeping the original exception as inner Exception
        static string GetMonthByID3(int month)
        {
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            try
            {
                return months[month - 1];
            }
            catch (IndexOutOfRangeException e)
            {
                throw new InvalidMonthArgument(month, $"{month} month is out of range!", e);
            }
        }

        //This example show how to catch a custom exception
        static void FifthTry()
        {
            while (true)
            {
                Console.WriteLine("Hi, Please type month number: ");
                int monthID = int.Parse(Console.ReadLine());
                try
                {
                    int yearPart = 12 * 100 / monthID;
                    Console.WriteLine($"{monthID} presents that  {yearPart}% of the year passed!");
                    string month = GetMonthByID2(monthID);
                    Console.WriteLine($"You chose {month}");
                }
                catch (InvalidMonthArgument e)
                {
                    Console.WriteLine($"Got Invalid Month Exception: {e.Message}");
                }
                catch (DivideByZeroException e)
                {
                    Console.WriteLine($"Tried to divide by zero!!: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    //This code will run in all cases (if exception was coaught or not)
                    Console.WriteLine("I am in the finnaly section");
                }
            }
        }

        //Show first how to build a custom exception and then the below example!
        //this example show how to throw an exception!
        static string GetMonthByID2(int month)
        {
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            if (month < 1 || month > 12)
                throw new InvalidMonthArgument(month, $"{month} month is out of range!");
            else
                return months[month - 1];
        }


        //This example show how to catch a specific exception or the general one
        static void ForthTry1()
        {
            while (true)
            {
                Console.WriteLine("Hi, Please type month number: ");
                int monthID = int.Parse(Console.ReadLine());
                try
                {
                    int yearPart = 12 * 100 / monthID;
                    Console.WriteLine($"{monthID} presents that  {yearPart}% of the year passed!");
                    string month = GetMonthByID(monthID);
                    Console.WriteLine($"You chose {month}");
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine($"Got Index Out of Range Exception: {e.Message}");
                }
                catch (DivideByZeroException e)
                {
                    Console.WriteLine($"Tried to divide by zero!!: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


            }
        }

        //this example shows how to catch specific exception
        //but note that other exceptions will not be caught!!!
        static void ForthTry()
        {
            while (true)
            {
                Console.WriteLine("Hi, Please type month number: ");
                int monthID = int.Parse(Console.ReadLine());
                try
                {
                    int yearPart = 12 * 100 / monthID;
                    Console.WriteLine($"{monthID} presents that  {yearPart}% of the year passed!");
                    string month = GetMonthByID(monthID);
                    Console.WriteLine($"You chose {month}");
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine($"Got Index Out of Range Exception: {e.Message}");
                }
                finally
                {
                    //This code will run in all cases (if exception was coaught or not)
                    Console.WriteLine("I am in the finnaly section");
                }
            }
        }

        //This example shows how exceptions are bubble up through the stack
        static void ThirdTry()
        {
            try
            {
                FirstTry();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

        //This example show how to catch an exception!
        static void SecondTry()
        {
            while (true)
            {
                Console.WriteLine("Hi, Please type month number: ");
                int monthID = int.Parse(Console.ReadLine());
                try
                {
                    string month = GetMonthByID(monthID);
                    Console.WriteLine($"You chose {month}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please type only numbers between 1 and 12");
                }
                finally
                {
                    //This code will run in all cases (if exception was coaught or not)
                    Console.WriteLine("I am in the finnaly section");
                }

                Console.WriteLine("After Try");
            }
        }


        //Code that may cause an exception
        //Month must be between 1 and 12
        static string GetMonthByID(int month)
        {
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            return months[month - 1];
        }

        static void FirstTry()
        {
            while (true)
            {
                Console.WriteLine("Hi, Please type month number: ");
                int monthID = int.Parse(Console.ReadLine());
                string month = GetMonthByID(monthID);
                Console.WriteLine($"You chose {month}");
            }
        }
        static void Run(string[] args)
        {
            SeventhTry();
        }


        //assignment solution
        static void DownloadFile(string url, string fileName)
        {
            bool success = true;
            WebClient client = new WebClient();
            try
            {
                client.DownloadFile(url, fileName);
            }
            catch (ArgumentNullException e)
            {
                success = false;
                Console.WriteLine($"ArgumentNull Exception: {e.Message}");
            }
            catch (WebException e)
            {
                success = false;
                Console.WriteLine($"WebException Exception: {e.Message}");
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine($"General Exception: {e.Message}");
            }
            finally
            {
                if (success)
                    Console.WriteLine("Download completed!");
                else
                    Console.WriteLine("Download Fail!");
            }

        }
    }
    
}
