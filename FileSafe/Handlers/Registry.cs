using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSafe.Handlers
{
    internal class Registry
    {

        // Specify the registry key path
        static string keyPath = @"SOFTWARE\FileSafe";

        // Specify the value name
        static string valueName = "Password";

        public static void Write(string valueData)
        {
            try
            {

                valueData = Handlers.Hashing.Hash(valueData);

                // Create or open the registry key
                using (RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(keyPath))
                {
                    // Set the value for the specified value name
                    key.SetValue(valueName, valueData);
                }

                //Console.WriteLine("Value successfully added to the Registry.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static bool Check()
        {
            try
            {
                // Open the registry key
                using (RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyPath))
                {
                    if (key != null)
                    {
                        // Check if the value exists
                        if (key.GetValue(valueName) != null)
                        {
                            // Value exists
                            Console.WriteLine("The value exists in the Registry.");
                            Info.Password = key.GetValue(valueName).ToString();
                            return true;
                        }
                        else
                        {
                            // Value does not exist
                            Console.WriteLine("The value does not exist in the Registry.");
                            return false;
                        }
                    }
                    else
                    {
                        // Key does not exist
                        Console.WriteLine("The specified key does not exist in the Registry.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

    }
}
