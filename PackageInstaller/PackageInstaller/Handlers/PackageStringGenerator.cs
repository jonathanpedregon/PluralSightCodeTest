using System;
using System.Collections.Generic;

namespace PackageInstaller.Handlers
{
    public interface IPackageStringGenerator
    {
        string GeneratePackageString(IList<string> packageList);
    }

    public class PackageStringGenerator : IPackageStringGenerator
    {
        public string GeneratePackageString(IList<string> packageList)
        {
            return string.Join(", ", packageList);
        }
    }
}
