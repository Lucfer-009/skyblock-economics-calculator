using System;

namespace GeneralUse
{

    /*
     
    Brad's Library 1.1
    Date of Last Addition : 08/02/2021

    */


    public class Input
    {
        public const string I_RELEASE = "1.0";

        public static int GetInt()
        {
            int number = 0;
            bool error = false;

            while (error == false)
            {

                try
                {
                    Display.ShowHeader();
                    number = Convert.ToInt32(Console.ReadLine());
                    error = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("*! Please enter an Integer !*");
                }
            }
            return number;
        }

        public static double GetDouble()
        {
            double number = 0.0;
            bool error = false;

            while (error == false)
            {

                try
                {
                    Display.ShowHeader();
                    number = Convert.ToDouble(Console.ReadLine());
                    error = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("*! Please enter a number !*");
                }
            }
            return number;
        }

        public static double GenerateRandomNo(int start, int end)
        {
            double number = 0.0;
            Random rand = new Random();

            number = rand.Next(start, end);

            return number;
        }

        public static char TestIfYesOrNo(string choice)
        {
            char state;
            while (true)
            {
                Display.ShowHeader();
                string response = Console.ReadLine().ToUpper();

                if (response == "Y" || response == "YES")
                {
                    state = 'Y';
                    break;
                }
                else if (response == "N" || response == "NO")
                {
                    state = 'N';
                    break;
                }
                else
                {
                    Console.WriteLine("*! Invalid response given, enter either Y/N !*");
                }

            }
            return state;

        }

    }
    public class Display
    {
        public const string D_RELEASE = "1.2";

        public static void ShowHeader()
        {
            Console.Write("> ");
        }

        public static string SecondsToNeatTime(int seconds)
        {
            string neatTime = "";

            int fortnight = seconds / 2419200;
            seconds %= 2419200;

            int week = seconds / 604800;
            seconds %= 604800;

            int days = seconds / 86400;
            seconds %= 86400;

            int hours = seconds / 3600;
            seconds %= 3600;

            int minutes = seconds / 60;
            seconds %= 60;

            neatTime = String.Format("{0} Fortnight(s) | {1} Weeks | {2, 1} Days | {3, 2} Hours | {4, 2} Minutes | {5, 2} Seconds", fortnight, week, days, hours, minutes, seconds);

            return neatTime;
        }
        public static void ShowError(string message)
        {
            Console.WriteLine($"*! {message} !*\n");
        }

    }
}