using System;
using System.Collections.Generic;
using System.Drawing;
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
            Profile profileToReturn = null;
            string filePathName = GetProfilePath();
            try
            {
                //If the file doesn't exist, create it with the default connection string.
                if (!File.Exists(filePathName))
                {
                    profileToReturn = GetDefaultProfile();
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Profile));

                    // Create an XmlTextReader to read with. 
                    using (var fs = new FileStream(filePathName, FileMode.Open, FileAccess.Read))
                    {
                        profileToReturn = (Profile)serializer.Deserialize(fs);
                    }

                    //Set any default values that might be missing.
                    UpgradeProfile(profileToReturn);
                }
            }
            catch (Exception ex)
            {
                profileToReturn = GetDefaultProfile();
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

        private static Profile GetDefaultProfile()
        {
            var ret = new Profile();
            
            //File types
            ret.AllowableFileTypes = new List<string>(new[] { ".flac", ".mp3", ".wav", ".wma" });

            //Theme
            ret.ForeColor = Color.FromArgb(255, 0, 255, 0);
            ret.BackColor = Color.Black;

            //Shortcut keys
            ret.PauseKey = Keys.Space;
            ret.PreviousKey = Keys.Z;
            ret.NextKey = Keys.X;

            return ret;
        }

        private static void UpgradeProfile(Profile profile)
        {
            //If we add new properties to a profile, users with existing profiles need the default values for these properties set.
            if (profile.AllowableFileTypes == null || profile.AllowableFileTypes.Count == 0)
            {
                profile.AllowableFileTypes = new List<string>(new[] {".flac", ".mp3", ".wav", ".wma"});
            }
            else if (profile.AllowableFileTypes.Contains("wav"))
            {
                // In version 1.0.4.0 and earlier the '.wav' extension was incorrectly added as 'wav'
                profile.AllowableFileTypes.Remove("wav");
                profile.AllowableFileTypes.Add(".wav");
            }
            
             //Shortcut keys
            if (profile.PauseKey == Keys.None)
                profile.PauseKey = Keys.Space;

            if (profile.PreviousKey == Keys.None)
                profile.PreviousKey = Keys.Z;

            if (profile.NextKey == Keys.None)
                profile.NextKey = Keys.X;

            if (profile.MiniPlayerKey == Keys.None)
                profile.MiniPlayerKey = Keys.M;   

        }
    }

}
