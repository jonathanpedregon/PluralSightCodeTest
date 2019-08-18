using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PackageInstaller.Exceptions;
using PackageInstaller.Handlers;
using PackageInstaller.Models;

namespace PackageInstallerTests.Controllers
{
    [TestFixture]
    public class PackageInstallerTests
    {
        private Mock<IPackageInputParser> InputParser { get; set; }
        private Mock<IPackageSorter> PackageSorter { get; set; }
        private Mock<IPackageStringGenerator> StringGenerator { get; set; }
        private PackageInstaller.Controllers.PackageInstaller PackageInstaller { get; set; }

        [SetUp]
        public void SetUp()
        {
            InputParser = new Mock<IPackageInputParser>();
            PackageSorter = new Mock<IPackageSorter>();
            StringGenerator = new Mock<IPackageStringGenerator>();
            PackageInstaller = new PackageInstaller.Controllers.PackageInstaller(InputParser.Object, PackageSorter.Object, StringGenerator.Object);
        }

        [Test]
        public void GetPackageInstallationString_ValidInput()
        {
            var input = new [] {"TestPackage: "};
            InputParser.Setup(x => x.GetPackagesFromInput(input)).Returns(new List<Package>());
            PackageSorter.Setup(x => x.GetOrderedPackages(new List<Package>())).Returns(new List<string>());
            StringGenerator.Setup(x => x.GeneratePackageString(new List<string>())).Returns("TestString");

            var installationString = PackageInstaller.GetPackageInstallationString(input);

            InputParser.Verify(x => x.GetPackagesFromInput(input), Times.Once);
            PackageSorter.Verify(x => x.GetOrderedPackages(new List<Package>()), Times.Once);
            StringGenerator.Verify(x => x.GeneratePackageString(new List<string>()), Times.Once);
            Assert.AreEqual("TestString", installationString);
        }

        [Test]
        public void GetPackageInstallationString_Exception()
        {
            var input = new[] { "TestPackage: " };
            InputParser.Setup(x => x.GetPackagesFromInput(input)).Returns(new List<Package>());
            var exceptionMessage = "Test Exception";
            PackageSorter.Setup(x => x.GetOrderedPackages(new List<Package>()))
                .Throws(new CircularDependencyException(exceptionMessage));

            var installationString = PackageInstaller.GetPackageInstallationString(input);

            InputParser.Verify(x => x.GetPackagesFromInput(input), Times.Once);
            PackageSorter.Verify(x => x.GetOrderedPackages(new List<Package>()), Times.Once);
            StringGenerator.VerifyNoOtherCalls();
            Assert.AreEqual(exceptionMessage, installationString);
        }
    }
}
