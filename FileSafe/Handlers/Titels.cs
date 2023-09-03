using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSafe.Handlers
{
    internal class Titels
    {

        public static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            foreach (string str in new List<string>()
            {

                "███████╗██╗██╗     ███████╗███████╗ █████╗ ███████╗███████╗",
                "██╔════╝██║██║     ██╔════╝██╔════╝██╔══██╗██╔════╝██╔════╝",
                "█████╗  ██║██║     █████╗  ███████╗███████║█████╗  █████╗  ",
                "██╔══╝  ██║██║     ██╔══╝  ╚════██║██╔══██║██╔══╝  ██╔══╝  ",
                "██║     ██║███████╗███████╗███████║██║  ██║██║     ███████╗",
                "╚═╝     ╚═╝╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝     ╚══════╝",


            }) Console.WriteLine(string.Format("{0," + (object)(Console.WindowWidth / 2 + str.Length / 2) + "}", (object)str));

            Console.ForegroundColor = ConsoleColor.White;

        }

    }
}
