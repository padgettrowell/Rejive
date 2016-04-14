using System;
using System.Windows.Forms;
using Rejive.Models;
using System.IO;
using System.Xml.Serialization;

namespace Rejive.Services
{
    public static class ProfileService
    {

        private static string _executingPath;

        private static string GetProfilePath()
        {
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
    }

}
