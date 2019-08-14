using PackageInstaller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageInstaller.Handlers
{
    public interface IPackageInputParser
    {
        IList<Package> GetPackagesFromInput(string[] userInput);
    }

    public class PackageInputParser : IPackageInputParser
    {
        public IList<Package> GetPackagesFromInput(string[] userInput)
        {
            throw new NotImplementedException();
        }
    }
}
