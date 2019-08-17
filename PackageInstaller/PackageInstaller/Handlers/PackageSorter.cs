using System;
using System.Collections.Generic;
using PackageInstaller.Models;

namespace PackageInstaller.Handlers
{
    public interface IPackageSorter
    {
        IList<string> GetOrderedPackages(IList<Package> packages);
    }

    public class PackageSorter : IPackageSorter

    {
        public IList<string> GetOrderedPackages(IList<Package> packages)
        {
            throw new NotImplementedException();
        }
    }
}
