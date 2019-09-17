using System;
using System.IO;
using vITGrid.Common.Tests;

namespace vITGrid.Log.Tests
{
    public class MockLog : IMock
    {
        private readonly string _testFilesPath;

        public MockLog()
        {
            _testFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestFiles");
        }

        public string GetTestFilesPath()
        {
            return Path.GetFullPath(_testFilesPath);
        }
    }
}