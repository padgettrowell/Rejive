using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Rejive
{
    public class ProfileService
    {

        private static string _executingPath;

        private static string GetProfilePath()
        {
            //return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\App.Profile";

            if (string.IsNullOrEmpty(_executingPath))
            {
                _executingPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            }

            return _executingPath + @"\.profile";
        }

        public static Profile LoadProfile()
        {
            Profile profileToReturn = new Profile();
            string filePathName = GetProfilePath();
            try
            {
                //If the file doesn't exist, create it with the default connection string.
                if (File.Exists(filePathName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Profile));

                    // Create an XmlTextReader to read with. 
                    using (var fs = new FileStream(filePathName, FileMode.Open, FileAccess.Read))
                    {
                        profileToReturn = (Profile)serializer.Deserialize(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(string.Format("Error loading profile: \n\n{0}\n\nDelete profile?", ex.Message), "Error loading profile, delete it?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    File.Delete(filePathName);
                }
            }

            return profileToReturn;
        }

        public static bool SaveProfile(Profile profile)
        {           

            string filePathName = GetProfilePath(); 

            try
            {
                //Delete the file if it already exists
                if (File.Exists(filePathName))
                {
                    File.Delete(filePathName);
                }

                //Create a new profile
                using (var fs = new FileStream(filePathName, FileMode.Create, FileAccess.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Profile));
                    serializer.Serialize(fs, profile);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error saving profile: \n\n{0}", ex.Message), "Profile Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }      
    }

}
