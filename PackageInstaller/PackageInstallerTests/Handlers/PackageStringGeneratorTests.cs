using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PackageInstaller.Handlers;

namespace PackageInstallerTests.Handlers
{
    [TestFixture]
    public class PackageStringGeneratorTests
    {
        private PackageStringGenerator StringGenerator { get; set; }

        [SetUp]
        public void SetUp()
        {
            StringGenerator = new PackageStringGenerator();
        }

        [Test]
        public void GeneratePackageString()
        {
            var packageList = new List<string>
            {
                "First",
                "Second",
                "Third"
            };
            var expectedString = "First, Second, Third";

            var packageString = StringGenerator.GeneratePackageString(packageList);

            Assert.AreEqual(expectedString, packageString);
        }
    }
}
