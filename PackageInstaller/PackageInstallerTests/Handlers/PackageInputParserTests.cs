using NUnit.Framework;
using PackageInstaller.Handlers;
using System.Linq;

namespace PackageInstallerTests.Handlers
{
    [TestFixture]
    public class PackageInputParserTests
    {

        private PackageInputParser PackageInputParser { get; set; }

        [SetUp]
        public void SetUp()
        {
            PackageInputParser = new PackageInputParser();
        }

        [Test]
        public void GetPackagesFromInput_FirstValidExample()
        {
            var input = new[] { "KittenService: CamelCaser", "CamelCaser: " };

            var packages = PackageInputParser.GetPackagesFromInput(input);

            Assert.AreEqual(1, packages.Count(x => x.Name == "KittenService" && x.Dependency == "CamelCaser"));
            Assert.AreEqual(1, packages.Count(x => x.Name == "CamelCaser" && x.Dependency == string.Empty));
            Assert.AreEqual(2, packages.Count);
        }

        [TestCase("KittenService: CamelCaser", "KittenService", "CamelCaser")]
        [TestCase("CamelCaser: ", "CamelCaser", "")]
        [TestCase("KittenService: ", "KittenService", "")]
        [TestCase("Leetmeme: Cyberportal", "Leetmeme", "Cyberportal")]
        public void GetPackageFromInput(string input, string expectedName, string expectedDependency)
        {
            var package = PackageInputParser.GetPackageFromInput(input);

            Assert.AreEqual(expectedName, package.Name);
            Assert.AreEqual(expectedDependency, package.Dependency);
        }
    }
}
