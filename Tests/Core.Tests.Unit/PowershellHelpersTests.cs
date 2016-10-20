using Bazooka.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests.Unit
{
    [TestClass]
    public class PowershellHelpersTests
    {
        [TestClass]
        public class Validate
        {
            [TestMethod]
            public void ShouldReturnTrueIfValid()
            {
                PowershellHelpers.Validate(@"Write-Host $PsVersion")
                                 .Should()
                                 .Be
                                 .True();
            }

            [TestMethod]
            public void ShouldReturnFalseIfItContainsAnError()
            {
                PowershellHelpers.Validate(@"param(a,)")
                 .Should()
                 .Be
                 .False();
            }
        }

        [TestClass]
        public class GetParseErrors
        {
            [TestMethod]
            public void ShouldReturnEmptyCollectionIfScriptCorrect()
            {
                PowershellHelpers.GetParseErrors(@"Write-Host $PsVersion")
                                 .Count
                                 .Should()
                                 .Be
                                 .EqualTo(0);
            }

            [TestMethod]
            public void ShouldReturnParseErrorsIfPresent()
            {
                PowershellHelpers.GetParseErrors(@"param(a,)")
                               .Count
                               .Should()
                               .Be
                               .EqualTo(3);              
            }
        }

        [TestClass]
        public class ExecuteScript
        {
            [TestMethod]
            public void ShouldLogErrorIfExitCode()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();

                PowershellHelpers.ExecuteScript(".", "throw \"test\"", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Error.Should().Be.True();

            }

            [TestMethod]
            public void ShouldRunScript()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();
                parameters.Add("username", "paolo");
                parameters.Add("password", "240686pn");
                PowershellHelpers.Execute(@"C:\\Users\Paolo\Downloads", "Install.ps1","test", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Error.Should().Be.True();

            }
        }

    }
}
