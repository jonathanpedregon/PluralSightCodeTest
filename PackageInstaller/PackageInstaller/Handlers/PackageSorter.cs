using System.Collections.Generic;
using System.Linq;
using PackageInstaller.Models;

namespace PackageInstaller.Handlers
{
    public interface IPackageSorter
    {
        IList<string> GetOrderedPackages(IList<Package> packages);
    }

    public class PackageSorter : IPackageSorter
    {
        public  IList<Package> Packages { get; set; }

        public IList<string> GetOrderedPackages(IList<Package> packages)
        {
            Packages = packages;
            var dependencyLists = new List<LinkedList<string>>();
            while (Packages.Any())
            {
                var firstPackage = PopFirstPackage();
                var dependencyList = InitializeDependencyList(firstPackage);
                dependencyLists.Add(GetDependencyList(dependencyList));
            }
            var combinedDependencyList = CombineDependencyLists(dependencyLists);
            return combinedDependencyList;
        }

        public static List<string> CombineDependencyLists(List<LinkedList<string>> dependencyLists)
        {
            var combinedDependencyList = new List<string>();
            foreach (var dependencyList in dependencyLists)
            {
                combinedDependencyList.AddRange(dependencyList);
            }
            return combinedDependencyList;
        }

        public static LinkedList<string> InitializeDependencyList(Package firstPackage)
        {
            var dependencyList = new LinkedList<string>();
            if (firstPackage.Dependency != string.Empty)
                dependencyList.AddFirst(firstPackage.Dependency);
            dependencyList.AddLast(firstPackage.Name);
            return dependencyList;
        }

        public Package PopFirstPackage()
        {
            var firstPackage = Packages.First();
            Packages.Remove(firstPackage);
            return firstPackage;
        }

        public LinkedList<string> GetDependencyList(LinkedList<string> dependencyList)
        {
            foreach (var package in Packages)
            {
                if (dependencyList.Contains(package.Name))
                {
                    AddPackageName(dependencyList, package);
                }
                else if (package.Dependency != string.Empty && dependencyList.Contains(package.Dependency))
                {
                    AddPackageDependency(dependencyList, package);
                }
                if (!Packages.Contains(package))
                    return GetDependencyList(dependencyList);
            }
            return dependencyList;
        }

        public void AddPackageDependency(LinkedList<string> dependencyList, Package package)
        {
            Packages.Remove(package);
            var packageNode = dependencyList.Find(package.Dependency);
            dependencyList.AddAfter(packageNode, package.Name);
        }

        public void AddPackageName(LinkedList<string> dependencyList, Package package)
        {
            Packages.Remove(package);
            if (package.Dependency != string.Empty)
            {
                var packageNode = dependencyList.Find(package.Name);
                dependencyList.AddBefore(packageNode, package.Dependency);
            }
        }
    }
}
