using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace DuplicateFinder
{
    public class FileSearchThread
    {
        public Dictionary<string, List<cFiles>> HashedFiles { get; private set; }

        public string RootDirectory { get; set; }
        public string CurrentFile { get; private set; }
        public bool Active { get; set; }

        public FileSearchThread()
        {
            HashedFiles = new Dictionary<string, List<cFiles>>();

            Active = false;
        }

        private string ByteToString(byte[] byteArray)
        {
            string hashString = "";

            foreach (byte b in byteArray)
            {
                hashString += b.ToString("x2");
            }

            return hashString;
        }

        public void ResetDictionary()
        {
            HashedFiles.Clear();
        }

        public void RecursiveSearch()
        {
            ResetDictionary();

            if (!string.IsNullOrEmpty(RootDirectory))
            {
                RecursiveSearch(RootDirectory);
            }

            Active = false;
        }

        public void RecursiveSearch(string dir)
        {
            var localFiles = Directory.GetFiles(dir);

            foreach (var localFile in localFiles)
            {
                if (Active)
                {
                    CurrentFile = localFile;
                    var fileObject = new cFiles(localFile, dir);
                    SHA256 sha256 = SHA256.Create();
                    var fileHash = ByteToString(sha256.ComputeHash(File.OpenRead(localFile)));

                    if (HashedFiles.ContainsKey(fileHash))
                    {
                        var list = HashedFiles[fileHash];
                        list.Add(fileObject);
                        HashedFiles[fileHash] = list;
                    }
                    else
                    {
                        var firstFile = new List<cFiles>();
                        firstFile.Add(fileObject);
                        HashedFiles.Add(fileHash, firstFile);
                    }
                }
                else
                {
                    break;
                }
            }

            if (Active)
            {
                var localDirectories = Directory.GetDirectories(dir);

                foreach (var localDirectory in localDirectories)
                {
                    RecursiveSearch(localDirectory);
                }
            }
        }
    }
}
