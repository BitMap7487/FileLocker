using FileSafe.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FileSafe.Modules
{
    internal class Unlock
    {

        public static void Start()
        {

            Console.Title = "Picture Safe | Unlock";

            Console.Clear();

            Handlers.Titels.MainMenu();

            Console.WriteLine("\n\n");

            Console.Write("Input password: ");

            string password = Console.ReadLine();

            if (Hashing.Verify(password, Info.Password) == false)
            {
                Console.WriteLine("Invalid password.");
                Thread.Sleep(3000);
                Program.Main();
                return;
            }

            string binaryFilePath = Info.BinDirectory; // Replace with the path of the combined binary file

            if (Handlers.Settings.EncryptBinFiles == true)
            {

                string[] split = Info.BinDirectory.Split('/');

                string Path = split[0];
                string FileName = split[1];

                binaryFilePath = Path + $"/ENCRYPTED {FileName}";

                EncryptionFiles.DecryptFile(binaryFilePath, binaryFilePath.Replace(".bin", ".dec"));
                Thread.Sleep(500);
                File.Delete(binaryFilePath);
                File.Move(binaryFilePath.Replace(".bin", ".dec"), binaryFilePath);

            }


            if (!File.Exists(binaryFilePath))
            {
                Console.WriteLine($"Didnt find {binaryFilePath}. Did you lock your files or did you lock it in Encrypt Bin mode?");
                Thread.Sleep(5000);
                Program.Main();
                return;
            }

            Console.WriteLine("Starting to unlock files.");

            try
            {
                using (StreamReader reader = new StreamReader(binaryFilePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string text = line;

                        // Read the next line (should contain file name and format)
                        line = reader.ReadLine();
                        if (line == null)
                        {
                            Console.WriteLine("Error: Invalid file format. Expecting both text and file name. 1");
                            break;
                        }

                        // Split the line to get the file name and format
                        string[] parts = line.Split(new[] { '.' }, 2);
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Error: Invalid file format. Expecting both text and file name. 2");
                            Console.ReadLine();
                            break;
                            
                        }

                        string fileName = parts[0];
                        string fileFormat = parts[1];

                        //text File Binary
                        //fileName
                        //fileFormat


                        if (fileName.Contains(Info.Directory) == true)
                            fileName.Replace(Info.Directory, "");


                        if (Settings.HideAndEncryptFiles == true)
                        {
                            text = EncryptionStrings.Decrypt(text);
                        }


                        byte[] byteArray = Convert.FromBase64String(text);

                        try
                        {
                            using (FileStream fileStream = new FileStream($"{fileName}.{fileFormat}", FileMode.Create, FileAccess.Write))
                            {
                                // Check if the byte array is not empty before writing
                                if (byteArray.Length > 0)
                                {
                                    fileStream.Write(byteArray, 0, byteArray.Length);
                                }
                                else
                                {
                                    Console.WriteLine("Error: The byte array is empty.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex}");
                            Console.ReadLine();
                        }

                    }

                    reader.Close();
                    reader.Dispose();

                    File.Delete(binaryFilePath);

                }

            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred: {ex}");
                Console.ReadLine();
            }


            Console.WriteLine("Done");
            Thread.Sleep(3000);
            Program.Main();

        }
    }
}
