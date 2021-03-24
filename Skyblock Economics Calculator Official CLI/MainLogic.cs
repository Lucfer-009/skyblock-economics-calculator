using System;
using System.IO;
using System.Linq;
using GeneralUse;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    class MainLogic
    {
        public static void SetDefaultSpeed(string material)
        {
            if(material == null)
            {
                Console.WriteLine(" : Enter the material you'd like to access your saved defualts for :");
                Console.WriteLine(" ( If the material doesn't exist a blank new file will be created  )");
                Display.ShowHeader();
                material = Console.ReadLine();
                
            }
            string path = FileLogic.MaterialToFilePath(material);

            if (FileLogic.CheckFileExistance(path) == false)
            {
                Console.WriteLine(" : No file detected, creating a blank save file :");
                FileLogic.FormatFileToSettingStandard(path, false);
            }

            int slot = SecondaryLogic.GetSlot(path, true);
            double speed = SecondaryLogic.RateCalculation(true,null);

            Console.WriteLine(" : Enter a label for this default : ");
            Display.ShowHeader();
            string comment = Console.ReadLine();

            string ammendment = Convert.ToString(speed);
            FileLogic.AmmendSettingsFile(path, ammendment, slot - 1, comment);
            Console.WriteLine(" : Default successfully saved :");


        }
        public static void MarginalGain()
        {
            Console.WriteLine(" : Enter Material : ");
            Display.ShowHeader();
            string material_A = Console.ReadLine();

            Console.WriteLine(" : Select / Enter Generation A :");
            double gen_A = SecondaryLogic.RateCalculation(false, material_A);

            Console.WriteLine(" : Select / Enter Generation B :");
            double gen_B = SecondaryLogic.RateCalculation(false, material_A);

            double difference = 0.0;
            string wordChoice = "";

            if(gen_B > gen_A)
            {
                wordChoice = "increase";
                difference = gen_B - gen_A;
            }
            else if (gen_A > gen_B)
            {
                wordChoice = "decrease";
                difference = gen_A = gen_B;
            }
            else
            {
                Display.ShowError("You've provided two identical generation setups");
                MarginalGain();
            }

            double percentageDifference = (difference / gen_A) * 100;

            Console.WriteLine(" : Provide the current Bazzar/Trading price of the item in it's basic form :");
            Display.ShowHeader();
            double tradePrice = Input.GetDouble();

            Console.WriteLine(String.Format($"\n - Generation B provides a {percentageDifference:N2}% {wordChoice} over Generation A"));

            Console.WriteLine("\n - GENERATION A -------------------------------------------------------\n");
            SecondaryLogic.FormatGenerationToConsole(gen_A, tradePrice, material_A);

            Console.WriteLine("\n - GENERATION B -------------------------------------------------------\n");
            SecondaryLogic.FormatGenerationToConsole(gen_B, tradePrice, material_A);

        }

        public static void RateOfProduction()
        {
            Console.WriteLine(" : Enter Material : ");
            Display.ShowHeader();
            string material = Console.ReadLine();

            double rate = SecondaryLogic.RateCalculation(false, material);

            Console.WriteLine(" : Provide the current Bazzar/Trading price of the item in it's basic form :");
            Display.ShowHeader();
            double tradePrice = Input.GetDouble();

            Console.WriteLine($"\n - Current Generation of {material}");
            SecondaryLogic.FormatGenerationToConsole(rate, tradePrice, material);
            Console.WriteLine();

        }
        public static void MaterialGoal()
        {
            Console.WriteLine(@"
| 1 - Base material
| 2 - Base Stack
| 3 - Enchanted V1
| 4 - Enchanted V1 Stack(s) 
| 5 - Enchanted V2 
| 6 - Enchanted V2 Stack(s) ");

            int choice;
            while(true)
            {
                Console.WriteLine(" : Enter the variant that you want to work with :");
                choice = Input.GetInt();
                if (choice > 6 || choice < 1)
                {
                    Display.ShowError("Out of the given bounds, (1-6)");
                }
                else { break; }
            }

            double[] variantConversion = { 1, 64, 160, 1024, 25600, 1638400 };
            double multiplactive = variantConversion[choice - 1];


            Console.WriteLine(" : Enter the amount you want to calculate :");
            double goal = Input.GetDouble();
            goal = goal * multiplactive;

            Console.WriteLine(" : Enter material : ");
            Display.ShowHeader();
            string material = Console.ReadLine();

            double rate = SecondaryLogic.RateCalculation(false, material); ;

            double secondsTillCompleted = goal / rate;
            Console.WriteLine($"--- {goal:N0} {material} at a speed of {rate:N2}/s would take ... ");
            Console.WriteLine(Display.SecondsToNeatTime(Convert.ToInt32(Math.Round(secondsTillCompleted, 0))));
            Console.WriteLine("---");

        }

        public static void ClearDefualts()
        {
            Console.WriteLine(@"
1 - Clear all settings
2 - Clear specific settings file
");
            int choice;
            while(true)
            {
                Console.WriteLine(" : Enter you choice from the menu :");
                choice = Input.GetInt();
                if (choice > 2 || choice < 1)
                {
                    Display.ShowError("Invalid input, (1-2)");
                }
                else { break; }
            }
            string material;
            string path;

            if (choice == 1)
            {
                Console.WriteLine(" : Are you sure you want to wipe the file (Y/N) : ");
                string YesOrNO = Console.ReadLine().ToLower();
                if (YesOrNO == "yes" || YesOrNO == "y")
                {
                    FileLogic.DeleteDir(FileLogic.SETTINGS_FOLDER);
                    Console.WriteLine(" - FILE RESET -");
                    FileLogic.CreateDirectory(FileLogic.SETTINGS_FOLDER);
                }
                else
                {
                    Console.WriteLine("- DELETION ABORTED -");
                }

            }
            else if (choice == 2)
            {
                Console.WriteLine(" : Enter material defaults you'd like to clear :");
                Display.ShowHeader();
                material = Console.ReadLine();
                path = FileLogic.MaterialToFilePath(material);
                if(FileLogic.CheckFileExistance(path) == false)
                {
                    Display.ShowError("This file doesn't exsist, and so can't be deleted");
                }
                else
                {
                    Console.WriteLine(" : Are you sure you want to wipe the file (Y/N) : ");
                    string YesOrNO = Console.ReadLine().ToLower();
                    if(YesOrNO == "yes" || YesOrNO == "y")
                    {
                        FileLogic.DeleteFile(path);
                        Console.WriteLine(" - FILE DELETED -");
                    }
                    else 
                    {
                        Console.WriteLine("- DELETION ABORTED -");
                    }
                }
                
            }



        }
    }
}
