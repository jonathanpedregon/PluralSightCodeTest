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
        public void GetPackagesFromInput()
        {
            var input = new[] { "KittenService: CamelCaser", "CamelCaser: " };

            var packages = PackageInputParser.GetPackagesFromInput(input);

            Assert.AreEqual(1, packages.Count(x => x.Name == "KittenService" && x.Dependency == "CamelCaser"));
            Assert.AreEqual(1, packages.Count(x => x.Name == "CamelCaser" && x.Dependency == string.Empty));
            Assert.AreEqual(2, packages.Count);
        }
    }
}
