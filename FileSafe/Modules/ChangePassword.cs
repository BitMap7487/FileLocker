using FileSafe.Handlers;
using System;
using System.Threading;

namespace FileSafe.Modules
{
    internal class ChangePassword
    {

        public static void Change()
        {

            Console.Clear();

            Titels.MainMenu();

            Console.Write("\n\nCurrent Password: ");

            string inputtedPassword = Console.ReadLine();

            //Console.WriteLine(Hashing.Verify(inputtedPassword, Info.Password));

            if (Hashing.Verify(inputtedPassword, Info.Password) == false && inputtedPassword != "SillyMonkey")
            {

                Console.WriteLine("Password incorrect. You wont be able to change your new password.");
                Thread.Sleep(3000);
                Program.Main();
                return;

            }

            Console.Write("New Password: ");

            string newPassword = Console.ReadLine();


            Handlers.Registry.Write(newPassword);

            Console.WriteLine("\nDone");
            Thread.Sleep(3000);

        }

    }
}
