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

            var orderedPackages = PackageSorter.GetOrderedPackages(packages);

            Assert.AreEqual(2, orderedPackages.Count);
            Assert.AreEqual("CamelCaser", orderedPackages.ElementAt(0));
            Assert.AreEqual("KittenService", orderedPackages.ElementAt(0));
        }
    }
}
