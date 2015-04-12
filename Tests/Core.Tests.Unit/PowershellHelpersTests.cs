using Bazooka.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;

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

    }
}
