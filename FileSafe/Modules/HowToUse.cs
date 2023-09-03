using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSafe.Modules
{
    internal class HowToUse
    {

        public static void Show()
        {

            Console.Clear();
            Handlers.Titels.MainMenu();
            Console.WriteLine("\n");

            Console.WriteLine("FileLocker - How to Use Guide\n");

            Console.WriteLine("Lock:");
            Console.WriteLine("The \"Lock\" feature allows you to hide all files within the directory where the FileLocker executable (EXE) is placed. When you activate this option, all the files in the folder will become invisible and inaccessible through regular file explorers.\n");

            Console.WriteLine("Unlock:");
            Console.WriteLine("To access your hidden files again, use the \"Unlock\" feature. It will prompt you to enter the correct password that you set during the initial setup. Once the correct password is provided, all the hidden files will become visible and accessible again.\n");

            Console.WriteLine("Change Password:");
            Console.WriteLine("The \"Change Password\" feature enables you to modify the password used to lock and unlock the hidden files. Make sure to choose a strong and memorable password to ensure the security of your files.\n");

            Console.WriteLine("\nContinue reading. Press enter\n");

            Console.ReadLine();

            Console.WriteLine("Settings:");
            Console.WriteLine("The Settings menu offers three options to customize the file protection according to your preferences:\n");

            Console.WriteLine("a. Strictly Hide Files:");
            Console.WriteLine("Selecting this option will only hide the files within the directory without encrypting them. The files will be invisible but will remain in their original state.\n");

            Console.WriteLine("b. Hide and Encrypt Files:");
            Console.WriteLine("This option not only hides the selected files but also encrypts them using a secure encryption algorithm. Encrypted files are much harder to decipher, providing a higher level of security for your sensitive data.\n");

            Console.WriteLine("c. EncryptBinFile:");
            Console.WriteLine("By choosing this option, FileLocker will encrypt the entire binary file containing the file binaries. This ensures that even if someone tries to access the binary file directly, the contents will remain secure and unreadable without the correct password.\n");

            Console.WriteLine("Remember to keep your password secure and do not share it with unauthorized individuals. If you forget your password, it might be impossible to recover the hidden files, especially if they are encrypted. So, keep a backup of important files outside of FileLocker if needed.");

            Console.WriteLine("\nEnjoy using FileLocker to protect your valuable files and maintain your privacy!\n\n");

            Console.WriteLine("Press enter to go back.");

            

            Console.ReadLine();

            Program.Main();

        }

    }
}
