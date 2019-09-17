using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vITGrid.Common.Tests.Log;

namespace vITGrid.Log.Tests
{
    [TestClass]
    public class UnitTestMockLog : IUnitTestMockLog
    {
        private string _testFilesPath;
        private MockLog _mockLog;

        [TestInitialize]
        public void TestInitialize()
        {
            _testFilesPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles"));

            _mockLog = new MockLog();
        }

        [TestMethod]
        public void TestMethodGetTestFilesPath()
        {
            Assert.AreEqual(_testFilesPath, _mockLog.GetTestFilesPath());
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }
    }
}