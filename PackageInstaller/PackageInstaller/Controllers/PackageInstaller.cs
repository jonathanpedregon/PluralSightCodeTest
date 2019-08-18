using PackageInstaller.Exceptions;
using PackageInstaller.Handlers;

namespace PackageInstaller.Controllers
{
    public interface IPackageInstaller
    {
        string GetPackageInstallationString(string[] userInput);
    }

    public class PackageInstaller : IPackageInstaller
    {
        private IPackageInputParser InputParser { get; set; }
        private IPackageSorter PackageSorter { get; set; }
        private IPackageStringGenerator StringGenerator { get; set; }

        public PackageInstaller(IPackageInputParser inputParser, IPackageSorter packageSorter, IPackageStringGenerator stringGenerator)
        {
            InputParser = inputParser;
            PackageSorter = packageSorter;
            StringGenerator = stringGenerator;
        }

        public string GetPackageInstallationString(string[] userInput)
        {
            var packages = InputParser.GetPackagesFromInput(userInput);
            try
            {
                var sortedPackages = PackageSorter.GetOrderedPackages(packages);
                return StringGenerator.GeneratePackageString(sortedPackages);
            }
            catch (CircularDependencyException e)
            {
                return e.Message;
            }
        }
    }
}
