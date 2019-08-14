namespace PackageInstaller.Models
{
    public class Package
    {
        public string Name { get; set; }
        public string Dependency { get; set; }

        public Package(string name, string dependency)
        {
            Name = name;
            Dependency = dependency;
        }
    }
}
