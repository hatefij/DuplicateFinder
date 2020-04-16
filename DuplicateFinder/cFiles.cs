namespace DuplicateFinder
{
    public class cFiles
    {
        public string Name { get; set; }
        public string Directory { get; set; }

        public cFiles(string name, string dir)
        {
            Name = name;
            Directory = dir;
        }
    }
}
