using System.Configuration;

namespace FileSafe.Handlers
{
    internal class Settings
    {

        // Getter and Setter for StrictlyHideFiles
        public static bool StrictlyHideFiles
        {
            get { return Properties.Settings.Default.StrictlyHideFiles; }
            set { Properties.Settings.Default.StrictlyHideFiles = value; }
        }

        // Getter and Setter for HideAndEncryptFiles
        public static bool HideAndEncryptFiles
        {
            get { return Properties.Settings.Default.HideAndEncryptFiles; }
            set { Properties.Settings.Default.HideAndEncryptFiles = value; }
        }

        // Getter and Setter for EncryptBinFiles
        public static bool EncryptBinFiles
        {
            get { return Properties.Settings.Default.EncryptBinFiles; }
            set { Properties.Settings.Default.EncryptBinFiles = value; }
        }

        public static bool Compress
        {
            get { return Properties.Settings.Default.Compress; }
            set { Properties.Settings.Default.Compress = value; }
        }

        public static string Key
        {
            get { return Properties.Settings.Default.Key; }
            set { Properties.Settings.Default.Key = value; }
        }

        public static string Passphrase
        {
            get { return Properties.Settings.Default.Passphrase; }
            set { Properties.Settings.Default.Passphrase = value; }
        }

        // Save the settings
        public static void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

    }
}
