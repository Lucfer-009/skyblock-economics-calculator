﻿using GeneralUse;
using System;
using System.Linq;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    internal class Boot
    {
        public static void Main()
        {
            Console.WriteLine("\nSEC Release 4.0 {Feb 2021}");
            Console.WriteLine("Created by Lucifer_009");

            if (FileLogic.CheckFolderExsistance(FileLogic.SETTINGS_FOLDER) == false) // Ensures a settings folder is there
            {                                                                            // Creates one if not there
                Display.ShowError("No main settings folder. Created one");
                FileLogic.CreateDirectory(FileLogic.SETTINGS_FOLDER);
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

    > Y. Save Minion Setups        <
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
                    MainLogic.MaterialGoal();
                    break;

                case "2":
                    MainLogic.RateOfProduction();
                    break;

                case "3":
                    MainLogic.MarginalGain();
                    break;

                case "Y":
                    MainLogic.SetDefaultSpeed(null);
                    break;

                case "Z":
                    MainLogic.ClearDefualts();
                    break;

                case "*":
                    Environment.Exit(1);
                    break;

                default:
                    Console.WriteLine("*! Error in MenuLogic !*\n");
                    break;
            }
            MenuLogic();
        }
    }
}