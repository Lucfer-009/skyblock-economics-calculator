using System;
using System.IO;
using System.Linq;
using GeneralUse;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    class MainLogic
    {
        public static void SetDefaultSpeed()
        {
            Console.WriteLine(" : Enter the material you'd like to access your saved defualts for :");
            Console.WriteLine(" ( If the material doesn't exist a blank new file will be created  )");
            Display.ShowHeader();
            string material = Console.ReadLine();
            string path = FileLogic.MaterialToFilePath(material);

            if(FileLogic.CheckFileExistance(path) == false)
            {
                Console.WriteLine(" : No file detected, creating a blank save file :");
                FileLogic.FormatFileToSettingStandard(path, false);
            }

            int slot = SecondaryLogic.GetSlot(path, true);
            double speed = SecondaryLogic.RateCalculation(true);

            Console.WriteLine(" : Enter a label for this default : ");
            Display.ShowHeader();
            string comment = Console.ReadLine();

            string ammendment = Convert.ToString(speed);
            FileLogic.AmmendSettingsFile(path, ammendment, slot - 1, comment);
            Console.WriteLine(" : Default successfully saved :");

            // Issue where origional contents of file will not remain present
            // ^^^^^^

        }


    }
}
