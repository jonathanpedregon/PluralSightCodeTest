using System;

namespace PackageInstaller.Controllers
{
    public interface IPackageInstaller
    {
        string InstallPackages(string userInput);
    }

    public class PackageInstaller : IPackageInstaller
    {
        public string InstallPackages(string userInput)
        {
            throw new NotImplementedException();
        }
    }
}
