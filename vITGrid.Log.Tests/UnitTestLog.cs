using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using vITGrid.Common.Log.Types;
using vITGrid.Common.Tests.Log;
using vITGrid.Common.Log.Interfaces;
using vITGrid.Log.Tests.Models;

namespace vITGrid.Log.Tests
{
    [TestClass]
    public class UnitTestLog : IUnitTestLog
    {
        private MockLog _mockLog;
        private ILog _log;

        private string _testFilesPath;
        
        [TestInitialize]
        public void TestInitialize()
        {
            _mockLog = new MockLog();
            _testFilesPath = _mockLog.GetTestFilesPath();
        }

        [TestMethod]
        public void TestMethodErrorLevels()
        {
            List<Error> errors = new List<Error>
            {
                new Error(ErrorType.Info, "info.txt", "Info Message", "INFO"),
                new Error(ErrorType.Warn, "warn.txt", "Warn Message", "WARN"),
                new Error(ErrorType.Error, "error.txt", "Error Message", "ERROR"),
                new Error(ErrorType.Fatal, "fatal.txt", "Fatal Message", "FATAL"),
                new Error(ErrorType.Debug, "debug.txt", "Debug Message", "DEBUG"),
                new Error(ErrorType.Trace, "trace.txt", "Trace Message", "TRACE")
            };

            const long archiveFileSize = 1;
            const int archiveFileCount = 5;
            foreach(Error error in errors)
            {
                string absoluteFileName = Path.Combine(_testFilesPath, error.FileName);
                File.Delete(absoluteFileName);

                Assert.IsFalse(File.Exists(absoluteFileName));

                _log = new Log(_testFilesPath, error.FileName, error.ErrorType, archiveFileSize, archiveFileCount);
                _log.LogEntry(error.ErrorMessage);

                Assert.IsTrue(File.Exists(absoluteFileName));

                string fileContent = File.ReadAllText(absoluteFileName);
                Assert.AreEqual(fileContent.Split("|")[1].Trim(), error.ErrorLabel);
                Assert.AreEqual(fileContent.Split("|")[4].Trim(), error.ErrorMessage);

                File.Delete(absoluteFileName);
            }
        }

        [TestMethod]
        public void TestMethodMaximumLogSize1Mb()
        {
            Error error = new Error(ErrorType.Info, "info.txt", "Info Message", "INFO");

            string absoluteFolderPath = Path.Combine(_testFilesPath, "logFiles");
            string absoluteFileName = Path.Combine(absoluteFolderPath, error.FileName);

            if(Directory.Exists(absoluteFolderPath))
            {
                Directory.Delete(absoluteFolderPath, true);
            }

            Assert.IsFalse(File.Exists(absoluteFileName));

            const int kb = 1024;
            const int tenMb = 14364;
            const long archiveFileSize = 1;
            const int archiveFileCount = 5;
            _log = new Log(absoluteFolderPath, error.FileName, error.ErrorType, archiveFileSize, archiveFileCount);
            for(int index = 0; index < tenMb; index++)
            {
                _log.LogEntry(error.ErrorMessage);
            }

            Assert.IsTrue(File.Exists(absoluteFileName));
            Assert.AreEqual(1, Directory.GetFiles(absoluteFolderPath).Length);

            long fileLength = new FileInfo(absoluteFileName).Length;
            Assert.AreEqual(archiveFileSize, Math.Ceiling((double)fileLength / (kb * kb)));

            _log.LogEntry(error.ErrorMessage);
            Assert.AreEqual(2, Directory.GetFiles(absoluteFolderPath).Length);

            Directory.Delete(absoluteFolderPath, true);
        }

        [TestMethod]
        public void TestMethodMaximum5ArchiveLogFiles()
        {
            Error error = new Error(ErrorType.Info, "info.txt", "Info Message", "INFO");

            string absoluteFolderPath = Path.Combine(_testFilesPath, "logFiles");

            if(Directory.Exists(absoluteFolderPath))
            {
                Directory.Delete(absoluteFolderPath, true);
            }

            const int tenMb = 14364;
            const long archiveFileSize = 1;
            const int archiveFileCount = 5;

            _log = new Log(absoluteFolderPath, error.FileName, error.ErrorType, archiveFileSize, archiveFileCount);
            for(int index = 0; index < (tenMb * archiveFileCount) + 1; index++)
            {
                _log.LogEntry(error.ErrorMessage);
            }
            Assert.AreEqual(6, Directory.GetFiles(absoluteFolderPath).Length);

            Directory.Delete(absoluteFolderPath, true);
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }
    }
}