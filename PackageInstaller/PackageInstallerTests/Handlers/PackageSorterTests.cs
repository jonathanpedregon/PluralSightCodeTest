using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var expectedPackages = new List<string> {"CamelCaser", "KittenService"};

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

        private void AssertCollectionsAreEqual(IList<string> expected, IList<string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }
    }
}
