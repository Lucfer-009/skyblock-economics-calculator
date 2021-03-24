using GeneralUse;
using System;
using System.IO;
using System.Linq;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    class SecondaryLogic
    {

        public static double loadMinionSave(bool Write, string material)
        {

            string path;
            while(true)
            {

                if (material == null)
                {
                    Console.WriteLine(" : Enter name of material to load defaults for : ");
                    Display.ShowHeader();
                    material = Console.ReadLine();
                }
                

                path = FileLogic.MaterialToFilePath(material);

                if (FileLogic.CheckFileExistance(path) == false)
                {
                    Display.ShowError("CRITICAL ERROR - No file in exsistance to load");
                    MainLogic.SetDefaultSpeed(material);
                }
                else
                {
                    break;
                }
            }

            int option = GetSlot(path, Write);

            string[,] settings = FileLogic.ReadFileToSettings(path);
            double speedSelected = Convert.ToDouble(settings[option-1, 0]);
            return speedSelected;


        }
        public static int GetSlot(string path, bool overwrite)
        {
            string[,] settings = FileLogic.ReadFileToSettings(path);
            bool EmptyPreset = true;

            Console.WriteLine("--- --- ---");
            for (int x = 0; x < settings.Length; x++)
            {
                try
                {
                    if (settings[x, 0] == null)
                    {
                        Console.WriteLine(@$"Preset {x + 1} : \EMPTY\");
                    }
                    else
                    {
                        Console.WriteLine($"Preset {x + 1} : {settings[x, 0]} | {settings[x, 1]}");
                        EmptyPreset = false;
                    }

                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }

            }
            Console.WriteLine("--- --- ---");

            int option;
            while (true)
            {
                Console.WriteLine(" : Enter the preset number you'd like to select : ");
                Display.ShowHeader();
                option = Input.GetInt();
                if (option < 1 || option > settings.Length && EmptyPreset == false)
                {
                    Display.ShowError("Entered an invalid response, number chosen outside the presets shown");
                }
                else if (EmptyPreset == true && overwrite == false)
                {
                    Display.ShowError("There is nothing saved into that preset");
                }
                else
                {
                    break;
                }
            }
            return option;
        }
        public static double RateCalculation(bool skipCheck, string material)
        {
            bool emptySettingsFolder = !Directory.EnumerateFiles(FileLogic.SETTINGS_FOLDER).Any();
            bool selectionSkip = false;
            bool check = false;
            double overallGenSpeed = 0.0;


            while(check == false)
            {
                if(skipCheck == false && emptySettingsFolder == false)
                {
                    Console.WriteLine(" : Defaults Detetced in Settings Folder. Want to access your defaults? Y/N :");
                    Display.ShowHeader();
                    string choice = Console.ReadLine().ToLower();
                    while(true)
                    {
                        if(choice == "y" || choice == "yes")
                        {
                            overallGenSpeed = loadMinionSave(false, material);
                            check = true;
                            break;
                        }
                        else if(choice == "n" || choice == "no")
                        {
                            selectionSkip = true;
                            check = true;
                            break;
                        }
                        else
                        {
                            Display.ShowError("Invalid Response. Enter Y/N");
                        }
                    }
                }
                else
                {
                    check = true;
                }
            }

            if (skipCheck == true || emptySettingsFolder == true || selectionSkip == true)
            {
                Console.WriteLine(" : Enter the number of minions being used : ");
                Display.ShowHeader();
                double noOfMinion = Input.GetDouble();

                while(true)
                {
                    Console.WriteLine(" : Are all minion speeds the same? [Y/N] : ");
                    Display.ShowHeader();
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "y" || choice == "yes")
                    {
                        Console.WriteLine(" : How long in seconds to produce one of the desired material : ");
                        Display.ShowHeader();
                        overallGenSpeed = (1 / Input.GetDouble()) * noOfMinion;
                        break;
                    }
                    else if (choice == "n" || choice == "no")
                    {
                        Console.WriteLine(" : How long in seconds to produce of of the desired material for the asked minion : ");
                        for (int x = 0; x < noOfMinion; x++)
                        {
                            Console.Write($" : Minion No {x + 1} : ");
                            overallGenSpeed += 1 / Input.GetDouble();
                        }
                        break;

                    }
                    else
                    {
                        Console.WriteLine("*! Please enter a valid response, either y/ n !*\n");
                    }
                }

                Console.WriteLine(" : Enter the percentage cost / loss of generation if present, else type 0 : ");
                Display.ShowHeader();
                double demeritPercentage = Input.GetDouble() / 100;
                overallGenSpeed = overallGenSpeed - (overallGenSpeed * demeritPercentage);


            }
            return overallGenSpeed;



        }

        public static void FormatGenerationToConsole(double rate, double tradePrice, string material)
        {
            double currentPricing, stackRate, enchantedV1Rate, enchantedV2Rate, doubleChest, enchantedV1RateStack, enchantedV2RateStack = 0;

            string[] timePeriod = { "Second", "Minute", "Hour", "12-Hour", "Day", "Week", "Fortnight", "Year" };
            int[] timeMultiplication = { 1, 60, 60, 12, 2, 7, 4, 13 };


            for (int x = 0; x < timePeriod.Length; x++)
            {
                rate *= timeMultiplication[x];
                currentPricing = tradePrice * rate;
                stackRate = rate / 64;
                doubleChest = stackRate / 54;

                enchantedV1Rate = stackRate / 2.5;
                enchantedV1RateStack = enchantedV1Rate / 64;

                enchantedV2Rate = enchantedV1RateStack / 2.5;
                enchantedV2RateStack = enchantedV2Rate / 64;

                Console.WriteLine($"-- -- PER {timePeriod[x]} -- --");
                Console.WriteLine(String.Format(" # {0,19:N2} BASE RATE             | @ {1,19:N2} STACK(S)               |", rate, stackRate));
                Console.WriteLine(String.Format(" $ {0,19:N2} COINS                 | / {1,19:N2} DOUBLE CHEST(S)        |", currentPricing, doubleChest));
                Console.WriteLine(String.Format(" ~ {0,19:N2} ENCHANTED V1          | ~ {1,19:N2} ENCHANTED V2           |", enchantedV1Rate, enchantedV2Rate));
                Console.WriteLine(String.Format(" ~ {0,19:N2} ENCHANTED V1 STACK(S) | ~ {1,19:N2} ENCHANTED V2 STACK(S)  |", enchantedV1RateStack, enchantedV2RateStack));
                Console.WriteLine();
            }
        }

    }
}
