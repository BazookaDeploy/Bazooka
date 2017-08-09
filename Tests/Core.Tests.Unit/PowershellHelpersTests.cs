using Bazooka.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;
using System.Collections.Generic;
using System.IO;
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
            public void ShouldLogErrorIfWriteError()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();

                PowershellHelpers.ExecuteScript(".", "Write-Error 'test'", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("test");
                logger.Logs.ElementAt(0).Error.Should().Be.True();
            }

            [TestMethod]
            public void ShouldLogOutputIfWriteOutput()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();
                parameters.Add("username", "paolo");

                PowershellHelpers.ExecuteScript(".", "Write-Output $username", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("paolo");
                logger.Logs.ElementAt(0).Error.Should().Be.False();
            }

            [TestMethod]
            public void ShouldLogOutputIfWriteHostParams()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();
                parameters.Add("username", "paolo");

                PowershellHelpers.ExecuteScript(".", "Write-Host $username", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("paolo");
                logger.Logs.ElementAt(0).Error.Should().Be.False();
            }

            [TestMethod]
            public void ShouldLogOutputIfWriteOutputParameters()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();

                PowershellHelpers.ExecuteScript(".", "Write-Output 'test'", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("test");
                logger.Logs.ElementAt(0).Error.Should().Be.False();
            }

            [TestMethod]
            public void ShouldLogOutputIfWriteHost()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();

                PowershellHelpers.ExecuteScript(".", "Write-Host 'test'", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("test");
                logger.Logs.ElementAt(0).Error.Should().Be.False();
            }

            [TestMethod]
            public void ShouldLogOutputofCommand()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();

                PowershellHelpers.ExecuteScript(".", "&echo 'test'", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("test");
                logger.Logs.ElementAt(0).Error.Should().Be.False();
            }

            [TestMethod]
            public void ShouldLogOrderedOutputRecognizingLevel()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();
                var script = @"Write-Output 'test'
Write-Error 'test2'
Write-Output 'test3'";
                PowershellHelpers.ExecuteScript(".", script, logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(3);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("test");
                logger.Logs.ElementAt(1).Text.Should().Be.EqualTo("test2");
                logger.Logs.ElementAt(2).Text.Should().Be.EqualTo("test3");
                logger.Logs.ElementAt(0).Error.Should().Be.False();
                logger.Logs.ElementAt(1).Error.Should().Be.True();
                logger.Logs.ElementAt(2).Error.Should().Be.False();
            }


            [TestMethod]
            public void ShouldRunScript()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();
                parameters.Add("username", "paolo");
                parameters.Add("password", "240686pn");

                var path = Path.GetTempPath();
                var fileName = Path.GetRandomFileName();
                File.WriteAllText(Path.Combine(path, fileName + ".ps1"), "Write-Output 'test'");
                PowershellHelpers.Execute(path, fileName + ".ps1", "test", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Error.Should().Be.False();
            }

            [TestMethod]
            public void ShouldRunScriptPassingParameters()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();
                parameters.Add("username", "paolo");
                parameters.Add("password", "240686pn");

                var path = Path.GetTempPath();
                var fileName = Path.GetRandomFileName();
                File.WriteAllText(Path.Combine(path, fileName + ".ps1"), "Write-Output $username");
                PowershellHelpers.Execute(path, fileName + ".ps1", "test", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Text.Should().Be.EqualTo("paolo");
                logger.Logs.ElementAt(0).Error.Should().Be.False();
            }

            [TestMethod]
            public void ShouldRunScriptAndError()
            {
                var logger = new StringLogger();
                var parameters = new Dictionary<string, string>();
                parameters.Add("username", "paolo");
                parameters.Add("password", "240686pn");

                var path = Path.GetTempPath();
                var fileName = Path.GetRandomFileName();
                File.WriteAllText(Path.Combine(path, fileName + ".ps1"), "Write-Error 'test'");
                PowershellHelpers.Execute(path, fileName + ".ps1", "test", logger, parameters);

                logger.Logs.Count.Should().Be.EqualTo(1);
                logger.Logs.ElementAt(0).Error.Should().Be.True();
            }
        }

    }
}
