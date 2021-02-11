using GeneralUse;
using System;
using System.IO;
using System.Linq;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    class SecondaryLogic
    {

        public static double loadMinionSave()
        {

            string path;
            while(true)
            {
                Console.WriteLine(" : Enter name of material to load defaults for : ");
                Display.ShowHeader();

                string material = Console.ReadLine();

                path = FileSystems.MaterialToFilePath(material);

                if (FileSystems.CheckFileExistance(path) == false)
                {
                    Display.ShowError("No file in exsistance to load");
                }
                else
                {
                    break;
                }
            }

            string[,] settings = FileSystems.ReadFileToSettings(path);

            Console.WriteLine("--- --- ---");
            for(int x = 0; x < settings.Length; x++)
            {
                try
                {
                    if(settings[x, 0] == null)
                    {
                        Console.WriteLine(@$"Preset {x+1} : \EMPTY\");
                    }
                    else
                    {
                        Console.WriteLine($"Preset {x + 1} : {settings[x, 0]} | {settings[x, 1]}");
                    }
                    
                }
                catch(IndexOutOfRangeException)
                {
                    break;
                }
                
            }
            Console.WriteLine("--- --- ---");

            int option;
            while(true)
            {
                Console.WriteLine(" : Enter the preset number you'd like to select : ");
                Display.ShowHeader();
                option = Input.GetInt();
                if(option < 1 || option > settings.Length)
                {
                    Display.ShowError("Entered an invalid response, number chosen outside the presets shown");
                }
                else if(settings[option-1, 0] == null)
                {
                    Display.ShowError("There is nothing saved into that preset");
                }
                else
                {
                    break;
                }
            }

            double speedSelected = Convert.ToDouble(settings[option-1, 0]);
            return speedSelected;


        }
        public static double RateCalculation(bool skipCheck)
        {
            bool emptySettingsFolder = !Directory.EnumerateFiles(FileSystems.SETTINGS_FOLDER).Any();
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
                            overallGenSpeed = loadMinionSave();
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

                Console.WriteLine(" : Are all minion speeds the same? [Y/N] : ");
                Display.ShowHeader();
                string choice = Console.ReadLine();

                if (choice == "y" || choice == "yes")
                {
                    Console.WriteLine(" : How long in seconds to produce one of the desired material : ");
                    Display.ShowHeader();
                    overallGenSpeed = (1 / Input.GetDouble()) * noOfMinion;
                }
                else if (choice == "n" || choice == "no")
                {
                    Console.WriteLine(" : How long in seconds to produce of of the desired material for the asked minion : ");
                    for (int x = 0; x < noOfMinion; x++)
                    {
                        Console.Write($" : Minion No {x + 1} : ");
                        overallGenSpeed += 1 / Input.GetDouble();
                    }

                }
                else
                {
                    Console.WriteLine("*! Please enter a valid response, either y/ n !*\n");
                }

                Console.WriteLine(" : Enter the percentage cost / loss of generation if present, else type 0 : ");
                Display.ShowHeader();
                double demeritPercentage = Input.GetDouble() / 100;
                overallGenSpeed = overallGenSpeed - (overallGenSpeed * demeritPercentage);


            }
            return overallGenSpeed;



        }



    }
}
