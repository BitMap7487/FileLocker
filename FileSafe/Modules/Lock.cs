using FileSafe.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileSafe.Modules
{
    internal class Lock
    {

        public static void Start()
        {


            Console.Clear();
            Titels.MainMenu();

            string sourceFolder = Info.Directory; // Replace with the path of the source folder
            string binaryFilePath = Info.BinDirectory; // Replace with the path where you want to save the combined binary file

            Console.WriteLine("\nStarting to lock files.");

            if(Handlers.Settings.EncryptBinFiles == true)
            {

                string[] split = Info.BinDirectory.Split('/');

                string Path = split[0];
                string FileName = split[1];

                binaryFilePath = Path + $"/ENCRYPTED {FileName}";

                Console.WriteLine("Encrypting Bin mode is ON!");

            }

            if (Settings.HideAndEncryptFiles == true)
            {
                Console.WriteLine("Encrypting Files is ON!");
            }

            ProcessDirectory(sourceFolder, binaryFilePath);


            if(Handlers.Settings.EncryptBinFiles == true)
            {

                EncryptionFiles.EncryptFile(binaryFilePath, binaryFilePath.Replace(".bin", ".enc"));
                Thread.Sleep(500);
                File.Delete(binaryFilePath);
                File.Move(binaryFilePath.Replace(".bin", ".enc"), binaryFilePath);

            }


            Console.WriteLine("Done");
            Thread.Sleep(3000);
            Program.Main();

        }


        static void ProcessDirectory(string sourceDirectory, string binaryFilePath)
        {
            // Get all the files in the source folder
            string[] files = Directory.GetFiles(sourceDirectory);

            if (files.Length > 0)
            {
                using (StreamWriter writer = new StreamWriter(binaryFilePath, true))
                {

                    foreach (string filePath in files)
                    {

                        string[] split = filePath.Split('.');
                        string fileName = string.Join(".", split.Take(split.Length - 1));
                        string fileFormat = split.Last();


                        if (filePath.Contains("FileSafe") || filePath.Contains("Files.bin"))
                            continue;

                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        string base64String = Convert.ToBase64String(fileBytes);

                        if(Settings.HideAndEncryptFiles == true)
                        {
                            base64String = EncryptionStrings.Encrypt(base64String);
                        }

                        writer.WriteLine(base64String);
                        writer.WriteLine($"{fileName}.{fileFormat}");

                        File.Delete(filePath);
                    }

                    writer.Flush();
                }
            }

            // Get all the directories in the source folder
            string[] directories = Directory.GetDirectories(sourceDirectory);

            foreach (string directoryPath in directories)
            {
                ProcessDirectory(directoryPath, binaryFilePath);
            }
        }

    }
}
