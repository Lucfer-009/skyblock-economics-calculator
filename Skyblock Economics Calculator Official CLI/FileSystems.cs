using GeneralUse;
using System;
using System.IO;
using System.Reflection;

namespace Skyblock_Economics_Calculator_Official_CLI
{
    internal class FileSystems
    {
        public static string MAIN_DIRECTORY = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string SETTINGS_FOLDER = Path.Combine(MAIN_DIRECTORY, "settings");
        public static int NO_OF_PRESETS = 8;

        public static bool CheckFileExistance(string path)
        {
            bool state = false;
            try
            {
                if (File.Exists(path))
                {
                    state = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("*! Error in CheckFileExsistance !*\n");
            }
            return state;
        }

        public static bool CheckFolderExsistance(string path)
        {
            bool state = false;
            try
            {
                if (Directory.Exists(path))
                {
                    state = true;
                }
            }
            catch (Exception)
            {
                Display.ShowError("Error in checking for directory exsistance");
            }
            return state;
        }

        public static void CreateFile(string path)
        {
            try
            {
                if (CheckFileExistance(path) == false)
                {
                    File.Create(path);
                    FormatFileToSettingStandard(path);
                }
                else
                {
                    Display.ShowError("File already exsists, can't create");
                }
            }
            catch
            {
                Display.ShowError("Error in creating file");
            }
        }

        public static void CreateDirectory(string path)
        {
            try
            {
                if (CheckFolderExsistance(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                else
                {
                    Display.ShowError("Directory already exsists, can't create");
                }
            }
            catch
            {
                Display.ShowError("Error in creating directory");
            }
        }

        public static void DeleteFileOrDir(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }
                else
                {
                    Console.WriteLine("!* No File or Directory in the given path to delete *!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("*! Error in Clearing the file or Directory !*");
            }
        }

        public static string[] ReadFileToSettings(string path)
        {

            int settingsLength = GetFileLength(path);
            string[] settings = new string[settingsLength];

            try
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    int index = 0;
                    string line = "";

                    for (int x = 0; x < settingsLength; x++)
                    {
                        line = sr.ReadLine();
                        index = line.IndexOf('=') + 2;
                        settings[x] = line.Substring(index);
                    }
                }
            }
            catch(Exception)
            {
                Display.ShowError("Error in Reading file to settings array");
            }
            return settings;
        }

        public static void FormatFileToSettingStandard(string path)
        {
            string[] baseSetting = new string[NO_OF_PRESETS];
            if (CheckFileExistance(path) == true)
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (int x = 0; x < baseSetting.Length; x++)
                    {
                        sw.WriteLine($"Preset Speed {x} = ");
                    }
                }
            }
            else
            {
                Console.WriteLine("*! Error formating settings file !*\n");
            }
        }

        public static int GetFileLength(string path)
        {
            int length = 0;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.ReadLine() != null)
                    {
                        length++;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("*! Error in reading length of file !*\n");
            }
            return length;
        }
    }
}