using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bazooka.Core;

namespace Core.Tests.Unit
{
    [TestClass]
    public class PackageHelpersTests
    {
        [TestClass]
        public class ExtractPackageName
        {
            [TestMethod]
            public void ShouldExtractSimpleName()
            {
                var name = PackageHelpers.ExtractPackageName("prova.1.2.3.nupkg");
                Assert.AreEqual(name, "prova");
            }

            [TestMethod]
            public void ShouldExtractCompositeName()
            {
                var name = PackageHelpers.ExtractPackageName("prova.test.1.2.3.nupkg");
                Assert.AreEqual(name, "prova.test");
            }
        }

        [TestClass]
        public class ExtractPackageVersion
        {
            [TestMethod]
            public void ShouldExtracteVersion()
            {
                var name = PackageHelpers.ExtractPackageVersion("prova.test.1.2.3.nupkg");
                Assert.AreEqual(name, "1.2.3");            
            }

            [TestMethod]
            public void ShouldExtractRCVersion()
            {
                var name = PackageHelpers.ExtractPackageVersion("prova.test.1.2.3-rc2.nupkg");
                Assert.AreEqual(name, "1.2.3-rc2");
            }
        }
    }
}
