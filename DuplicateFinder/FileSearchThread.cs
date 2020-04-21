using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DuplicateFinder
{
    public class FileSearchThread
    {
        public Dictionary<string, List<cFiles>> HashedFiles { get; private set; }

        public string RootDirectory { get; set; }
        public string CurrentFile { get; private set; }
        public double NumberOfFiles { get; private set; }
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

            NumberOfFiles = 0;
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
            try
            {
                var localFiles = Directory.GetFiles(dir);

                foreach (var localFile in localFiles)
                {
                    CurrentFile = localFile;

                    if (File.Exists(CurrentFile)) // sanity check
                    {
                        NumberOfFiles++;
                        var fileObject = new cFiles(localFile, dir);
                        SHA256 sha256 = SHA256.Create();
                        var fileHash = ByteToString(sha256.ComputeHash(File.OpenRead(localFile)));

                        if (Active) // continue if the thread should remain active
                        {
                            if (HashedFiles.ContainsKey(fileHash))
                            {
                                var list = HashedFiles[fileHash];
                                list.Add(fileObject);

                                try // prevent crash in the case where the primary thread has stopped and cleared the dictionary before the current thread has stopped
                                {
                                    HashedFiles[fileHash] = list;
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Error adding new file to existing file hash. Another thread may have cleared the dictionary.");
                                }
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
                }

                if (Active) // continue if the thread should remain active
                {
                    var localDirectories = Directory.GetDirectories(dir);

                    foreach (var localDirectory in localDirectories)
                    {
                        RecursiveSearch(localDirectory);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
        }
    }
}
