using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
