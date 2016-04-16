using System;
using System.IO;
using System.Collections.Generic;

namespace Rejive
{
    public static class FileSearcher
    {
        public static IEnumerable<string> Search(string root, string searchPattern, List<string> allowableFileTypes)
        {
            Queue<string> dirs = new Queue<string>();
            dirs.Enqueue(root);
            while (dirs.Count > 0)
            {
                string dir = dirs.Dequeue();

                // files
                string[] paths = null;
                try
                {
                    if (string.IsNullOrEmpty(searchPattern))
                    {
                        paths = Directory.GetFiles(dir);    
                    }
                    else
                    {
                        paths = Directory.GetFiles(dir, searchPattern);
                    }
                }
                catch { } // swallow

                if (paths != null && paths.Length > 0)
                {
                    foreach (string file in paths)
                    {
                        if (allowableFileTypes != null && allowableFileTypes.Contains(Path.GetExtension(file)))
                        {
                            yield return file;    
                        }
                    }
                }

                // sub-directories
                paths = null;
                try
                {
                    paths = Directory.GetDirectories(dir);
                }
                catch { } // swallow

                if (paths != null && paths.Length > 0)
                {
                    foreach (string subDir in paths)
                    {
                        dirs.Enqueue(subDir);
                    }
                }
            }
        }
    }
}
