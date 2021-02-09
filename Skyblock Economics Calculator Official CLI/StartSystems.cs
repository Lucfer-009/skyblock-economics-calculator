using GeneralUse;
using System;
using System.IO;
using System.Linq;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    internal class Boot
    {
        public static void Main()
        {
            Console.WriteLine("\nSEC Release 4.0 {Feb 2021}");
            Console.WriteLine("Created by Lucifer_009");

            if (FileSystems.CheckFolderExsistance(FileSystems.SETTINGS_FOLDER) == false) // Ensures a settings folder is there
            {                                                                           // Creates one if not there
                Display.ShowError("No main settings folder. Created one");
                FileSystems.CreateDirectory(FileSystems.SETTINGS_FOLDER);
            }

            LaunchMenu.MenuLogic();

        }
    }

    internal class LaunchMenu
    {
        public static void ShowMenu()
        {
            Console.WriteLine(@"
    > 1. Material Goal             <
    > 2. Rate of Production        <
    > 3. Marginal Gain             <

    > X. Set Default Material      <
    > Y. Set Default Minion Setup  <
    > Z. Clear Defaults            <

    > *. Quit                      <

            ");
        }

        public static void MenuLogic()
        {
            ShowMenu();

            string choice;
            string[] possibleChoices = { "1", "2", "3", "X", "Y", "Z", "*" };

            while (true)
            {
                Console.WriteLine(" : Enter a menu option : ");
                Display.ShowHeader();
                choice = Console.ReadLine().ToUpper();
                if (possibleChoices.Contains(choice))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("*! Invalid Response, enter an appropriate menu option !*\n");
                }
            }

            switch (choice)
            {
                case "1":

                    break;

                case "2":

                    break;

                case "3":

                    break;

                case "X":

                    break;

                case "Y":

                    break;

                case "Z":

                    break;

                case "*":
                    break;

                default:
                    Console.WriteLine("*! Error in MenuLogic !*\n");
                    break;
            }
        }
    }
}