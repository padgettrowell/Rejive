using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rejive.Models
{
    public class Profile
    {
        private List<string> _allowableFileTypes;

        public Profile()
        {
            _allowableFileTypes = new List<string>(new[] { ".flac", ".mp3", ".wav", ".wma" });
        }

        public List<string> AllowableFileTypes
        {
            get { return _allowableFileTypes; }
        }
    }
}
