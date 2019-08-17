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

        public override bool Equals(object obj)
        {
            if (!(obj is Package otherPackage))
                return false;
            return Name == otherPackage.Name && Dependency == otherPackage.Dependency;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Dependency.GetHashCode();
        }
    }
}
