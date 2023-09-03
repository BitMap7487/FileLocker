using FileSafe.Handlers;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace FileSafe.Modules
{
    internal class SettingsMenu
    {

        public static void Show()
        {



            int selectedOption = 0;
            int totalOptions = 9; // Including the Exit option

            while (true)
            {


                Console.Clear();
                Handlers.Titels.MainMenu();

                Console.WriteLine("\nSettings menu. Arrow Up/Down to select, Enter to change.");

                // Display the settings with their current states and highlight the selected one
                for (int i = 0; i < totalOptions; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    switch (i)
                    {
                        case 0:
                            Console.WriteLine($" {(Handlers.Settings.StrictlyHideFiles ? "X" : " ")} 1. Strictly Hide Files");
                            break;
                        case 1:
                            Console.WriteLine($" {(Handlers.Settings.HideAndEncryptFiles ? "X" : " ")} 2. Hide and Encrypt Files");
                            break;
                        case 2:
                            Console.WriteLine($" {(Handlers.Settings.EncryptBinFiles ? "X" : " ")} 3. Encrypt .bin Files");
                            break;
                        case 3:
                            Console.WriteLine($" {(Handlers.Settings.Compress ? "X" : " ")} 4. Compress .bin Files");
                            break;
                        case 4:
                            Console.WriteLine("   5. Change Password");
                            
                            break;
                        case 5:
                            Console.WriteLine("   6. Change Encryption Key");
                            break;
                        case 6:
                            Console.WriteLine("   7. Change Encryption Passphrase");
                            break;
                        case 7:
                            Console.WriteLine("   8. Quit Without Saving");
                            break;
                        case 8:
                            Console.WriteLine("   9. Save And Quit");
                            break;
                    }

                    Console.ResetColor();
                }

                // Handle arrow key navigation
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption - 1 + totalOptions) % totalOptions;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption + 1) % totalOptions;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedOption == 8)
                        {
                            //Save and Quit
                            Handlers.Settings.SaveSettings();
                            Program.Main();
                            return;
                        }
                        else if (selectedOption == 7)
                        {
                            //Quit
                            Program.Main();
                            return;
                        }
                        else
                        {
                            // Toggle the selected option
                            switch (selectedOption)
                            {
                                case 0:

                                    if (Handlers.Settings.HideAndEncryptFiles == false)
                                        break;

                                    Handlers.Settings.StrictlyHideFiles = !Handlers.Settings.StrictlyHideFiles;
                                    Handlers.Settings.HideAndEncryptFiles = false; // Deselect the other option
                                    break;
                                case 1:
                                    if (Handlers.Settings.StrictlyHideFiles == false)
                                        break;
                                    Handlers.Settings.HideAndEncryptFiles = !Handlers.Settings.HideAndEncryptFiles;
                                    Handlers.Settings.StrictlyHideFiles = false; // Deselect the other option
                                    break;
                                case 2:
                                    Handlers.Settings.EncryptBinFiles = !Handlers.Settings.EncryptBinFiles;
                                    break;

                                case 3:
                                    Handlers.Settings.Compress = !Handlers.Settings.Compress;
                                    break;

                                case 4:
                                    ChangePassword.Change();
                                    break;
                                case 5:

                                    Console.Clear();

                                    Titels.MainMenu();

                                    Console.WriteLine("\n\nInsert new Key (Allowed characters: 0123456789ABCDEF, Length: 32 Characters).");
                                    Console.Write("> ");
                                    string newKey = Console.ReadLine();

                                    string pattern = "[^0-9A-F]";

                                    // Use the Regex.IsMatch method to check if the input contains any invalid characters
                                    if (Regex.IsMatch(newKey, pattern, RegexOptions.IgnoreCase))
                                    {
                                        Console.WriteLine("Input contains invalid characters.");
                                        Thread.Sleep(2000);
                                    }
                                    else if (newKey.Length != 32)
                                    {
                                        Console.WriteLine("Input should be exactly 32 characters long.");
                                        Thread.Sleep(2000);
                                    }
                                    else
                                    {
                                        Handlers.Settings.Key = newKey;

                                        Console.WriteLine($"Key changed to: {newKey}");
                                        Thread.Sleep(2000);

                                    }

                                    break;

                                case 6:

                                    Console.Clear();

                                    Titels.MainMenu();

                                    Console.WriteLine("\n\nInsert new Passphrase");
                                    Console.Write("> ");
                                    string newPassphrase = Console.ReadLine();

                                    Handlers.Settings.Passphrase = newPassphrase;
                                    
                                    break;
                            }
                        }
                    break;
                    case ConsoleKey.Q:
                        Program.Main();
                    break;
                }
            }
        }

    }


}