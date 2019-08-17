using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PackageInstaller.Handlers;
using PackageInstaller.Models;

namespace PackageInstallerTests.Handlers
{
    [TestFixture]
    public class PackageSorterTests
    {
        private PackageSorter PackageSorter { get; set; }

        [SetUp]
        public void SetUp()
        {
            PackageSorter = new PackageSorter();
        }

        [Test]
        public void GetOrderedPackages_FirstExample()
        {
            var packages = new List<Package>
            {
                new Package("KittenService", "CamelCaser"),
                new Package("CamelCaser", "")
            };
            var expectedPackages = new List<string> { "CamelCaser", "KittenService" };

            var orderedPackages = PackageSorter.GetOrderedPackages(packages);

            AssertCollectionsAreEqual(expectedPackages, orderedPackages);
        }

        [Test]
        public void GetOrderedPackages_SecondExample()
        {
            var packages = new List<Package>
            {
                new Package("KittenService", ""),
                new Package("Leetmeme", "Cyberportal"),
                new Package("Cyberportal", "Ice"),
                new Package("CamelCaser", "KittenService"),
                new Package("Fraudstream", "Leetmeme"),
                new Package("Ice", "")
            };
            var expectedPackages = new List<string> { "KittenService", "CamelCaser", "Ice", "Cyberportal", "Leetmeme", "Fraudstream" };

            var orderedPackages = PackageSorter.GetOrderedPackages(packages);

            AssertCollectionsAreEqual(expectedPackages, orderedPackages);
        }

        [Test]
        public void AddPackageDependency()
        {
            PackageSorter.Packages = new List<Package>();
            var dependencyList = new LinkedList<string>();
            dependencyList.AddFirst("CamelCaser");
            var package = new Package("KittenService", "CamelCaser");
            var expectedLinkedList = new LinkedList<string>();
            expectedLinkedList.AddFirst("CamelCaser");
            expectedLinkedList.AddLast("KittenService");

            PackageSorter.AddPackageDependency(dependencyList, package);

            AssertCollectionsAreEqual(expectedLinkedList, dependencyList);
        }

        [Test]
        public void AddPackageName()
        {
            PackageSorter.Packages = new List<Package>();
            var dependencyList = new LinkedList<string>();
            dependencyList.AddFirst("CamelCaser");
            var package = new Package("CamelCaser", "KittenService");
            var expectedValues = new List<string> { "KittenService", "CamelCaser" };
            var expectedLinkedList = new LinkedList<string>(expectedValues);


            PackageSorter.AddPackageName(dependencyList, package);

            AssertCollectionsAreEqual(expectedLinkedList, dependencyList);
        }

        [Test]
        public void PopFirstPackage()
        {
            var expectedPackage = new Package("TestPackage", "TestDependency");
            PackageSorter.Packages = new List<Package> { expectedPackage };

            var actualPackage = PackageSorter.PopFirstPackage();

            Assert.AreEqual(expectedPackage, actualPackage);
            Assert.IsFalse(PackageSorter.Packages.Any());
        }

        [Test]
        public void InitializeDependencyList_WithDependency()
        {
            var package = new Package("TestPackage", "TestDependency");

            var dependencyList = PackageSorter.InitializeDependencyList(package);

            Assert.AreEqual(2, dependencyList.Count);
            Assert.AreEqual("TestDependency", dependencyList.ElementAt(0));
            Assert.AreEqual("TestPackage", dependencyList.ElementAt(1));
        }

        [Test]
        public void InitializeDependencyList_NoDependency()
        {
            var package = new Package("TestPackage", "");

            var dependencyList = PackageSorter.InitializeDependencyList(package);

            Assert.AreEqual(1, dependencyList.Count);
            Assert.AreEqual("TestPackage", dependencyList.ElementAt(0));
        }

        [Test]
        public void CombineDependencyLists()
        {
            var dependencyCollection = GetDependencyCollection();
            var expectedCombinedList = new List<string>{"One", "Two", "Three", "Four", "Five"};

            var combinedDependencyList = PackageSorter.CombineDependencyLists(dependencyCollection);

            AssertCollectionsAreEqual(expectedCombinedList, combinedDependencyList);

        }

        private static List<LinkedList<string>> GetDependencyCollection()
        {
            var firstDependencies = new List<string> {"One", "Two"};
            var firstDependencyList = new LinkedList<string>(firstDependencies);
            var secondDependencies = new List<string> {"Three", "Four", "Five"};
            var secondDependencyList = new LinkedList<string>(secondDependencies);
            var dependencyCollection = new List<LinkedList<string>>
            {
                firstDependencyList,
                secondDependencyList
            };
            return dependencyCollection;
        }

        private void AssertCollectionsAreEqual(ICollection<string> expected, ICollection<string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }
    }
}
