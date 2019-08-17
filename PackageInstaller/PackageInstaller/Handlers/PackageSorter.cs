using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PackageInstaller.Models;

namespace PackageInstaller.Handlers
{
    public interface IPackageSorter
    {
        IList<string> GetOrderedPackages(IList<Package> packages);
    }

    public class PackageSorter : IPackageSorter
    {
        private IList<Package> Packages { get; set; }

        public IList<string> GetOrderedPackages(IList<Package> packages)
        {
            Packages = packages;
            var dependencyLists = new List<LinkedList<string>>();
            while (Packages.Any())
            {
                var linkedList = new LinkedList<string>();
                var firstPackage = Packages.First();
                Packages.Remove(firstPackage);
                if (firstPackage.Dependency != string.Empty)
                    linkedList.AddFirst(firstPackage.Dependency);
                linkedList.AddLast(firstPackage.Name);
                
                dependencyLists.Add(GetDependencyList(linkedList));
            }
            var combinedDependencyList = new List<string>();
            foreach (var dependencyList in dependencyLists)
            {
                combinedDependencyList.AddRange(dependencyList);
            }

            return combinedDependencyList;
        }

        public LinkedList<string> GetDependencyList(LinkedList<string> dependencyList)
        {
            foreach (var package in Packages)
            {
                if (dependencyList.Contains(package.Name))
                {
                    Packages.Remove(package);
                    if (package.Dependency != string.Empty)
                    {
                        var packageNode = dependencyList.Find(package.Name);
                        dependencyList.AddBefore(packageNode, package.Dependency);
                    }
                }
                else if (package.Dependency != string.Empty && dependencyList.Contains(package.Dependency))
                {
                    Packages.Remove(package);
                    var packageNode = dependencyList.Find(package.Dependency);
                    dependencyList.AddAfter(packageNode, package.Name);
                }
                if (!Packages.Contains(package))
                    return GetDependencyList(dependencyList);
            }
            return dependencyList;
        }
    }
}
