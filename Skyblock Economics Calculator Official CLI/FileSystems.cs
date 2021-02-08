using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    class FileSystems
    {
        public static string MAIN_DIRECTORY = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string SETTINGS_FOLDER = Path.Combine(MAIN_DIRECTORY, "settings");

        public static void CheckFileExistance(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("*! Error in CheckFileExsistance !*\n");
            }
            
        }
        public static void CheckFolderExsistance(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("*! Error in CheckFolderExsistance !*\n");
            }
            
        }


    }
}
