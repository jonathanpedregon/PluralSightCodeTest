using PackageInstaller.Models;
using System.Collections.Generic;

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
            var packages = new List<Package>();
            foreach (var userInputString in userInput)
            {
                var splitString = userInputString.Split(':');
                var packageName = splitString[0].Trim();
                var dependency = splitString[1].Trim();
                var package = new Package(packageName, dependency);
                packages.Add(package);
            }
            return packages;
        }
    }
}
