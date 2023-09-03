using System;
using System.Collections.Generic;
using System.IO;

namespace FileSafe
{
    internal class Program
    {
        public static void Main()
        {

            if(Handlers.Registry.Check() == false)
            {

                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string folderName = "FileSafe"; // Replace with your application name
                string folderPath = Path.Combine(appDataPath, folderName);

                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating folder: {ex.Message}");
                }

                Console.Write("Hello user.\nPlease input a password you would like to use:");

                string inputtedPassword = Console.ReadLine();

                Handlers.Registry.Write(inputtedPassword);
            }


            Console.Clear();

            Handlers.Info.Directory = Environment.CurrentDirectory;
            string appDataPath1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderName1 = "FileSafe"; // Replace with your application name
            string folderPath1 = Path.Combine(appDataPath1, folderName1);


            string FileName = string.Join("_", Handlers.Info.Directory.Split(Path.GetInvalidFileNameChars()));

            Handlers.Info.BinDirectory = $"{folderPath1}/{FileName}.bin";


            Console.Title = "Picture Safe";

            List<string> menuItems = new List<string>()
            {
                "1 > Lock",
                "2 > Unlock",
                "3 > How to use?",
                "4 > Settings",
                "5 > Quit"
            };

            int selectedOption = 0;
            int totalOptions = menuItems.Count;

            while (true)
            {

                Console.Clear();
                Handlers.Titels.MainMenu();

                Console.WriteLine("\n");

                Console.Write(string.Format("{0," + (Console.WindowWidth / 2 + "╔════════════════════════════════════════════════════════════════════════════════════╗".Length / 2) + "}", "╔════════════════════════════════════════════════════════════════════════════════════╗\n"));

                Console.Write(string.Format("{0," + (Console.WindowWidth / 2 + "║                                                                                    ║".Length / 2) + "}", "║                                                                                    ║\n"));

                // Display the menu with the ">" symbol in front of the selected option
                for (int i = 0; i < totalOptions; i++)
                {

                    int itemLength = menuItems[i].Length;
                    int desiredLength = 52 - itemLength;

                    string spaces = "";

                    for (int j = 0; j < desiredLength; j++)
                    {
                        spaces += " ";
                    }



                    if (i == selectedOption)
                    {

                        Console.Write(string.Format("{0," + (Console.WindowWidth / 2 + $"║                              > {menuItems[i]}{spaces}║".Length / 2) + "}", $"║                              > {menuItems[i]}{spaces}║\n"));
                    }
                    else
                    {
                        Console.Write(string.Format("{0," + (Console.WindowWidth / 2 + $"║                                {menuItems[i]}{spaces}║".Length / 2) + "}", $"║                                {menuItems[i]}{spaces}║\n"));
                    }
                }

                Console.Write(string.Format("{0," + (Console.WindowWidth / 2 + "║                                                                                    ║".Length / 2) + "}", "║                                                                                    ║\n"));

                Console.Write(string.Format("{0," + (Console.WindowWidth / 2 + "╚════════════════════════════════════════════════════════════════════════════════════╝".Length / 2) + "}", "╚════════════════════════════════════════════════════════════════════════════════════╝\n"));

                // Wait for the user to press a key
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption - 1 + totalOptions) % totalOptions;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption + 1) % totalOptions;
                        break;
                    case ConsoleKey.Enter:
                        PerformAction(selectedOption);
                        break;
                }
            }

        }

        static void PerformAction(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    Modules.Lock.Start();
                    break;
                case 1:
                    Modules.Unlock.Start();
                    break;
                case 2:
                    Modules.HowToUse.Show();
                    break;
                case 3:
                    Modules.SettingsMenu.Show();
                    break;
                case 4:
                    Environment.Exit(1);
                    break;
            }
        }

    }
}
