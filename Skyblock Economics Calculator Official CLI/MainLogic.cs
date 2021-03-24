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

            Console.WriteLine("\n - Current Generation of ");
        }

    }
}
