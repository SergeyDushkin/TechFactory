using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace TF.Data.Systems.IO.FileStorage.Test
{
    [TestClass]
    public class FileStorageServiceTest
    {
        readonly IStorageService service;

        readonly string root;
        readonly string _existFilePath1;
        readonly string _existFilePath2;
        readonly string _existFilePath3;
        readonly string _existFilePath4;
        readonly string _noExistFilePath;
        readonly string _existFolderPath1;
        readonly string _existFolderPath2;
        readonly string _noExistFolderPath;

        public FileStorageServiceTest()
        {
            root = Path.Combine(Directory.GetCurrentDirectory(), "TEST");

            _existFolderPath1 = "folder1";
            _existFolderPath2 = "folder2";
            _noExistFolderPath = Path.Combine(root, "folder3");

            _existFilePath1 = Path.Combine(_existFolderPath1, "file1.txt");
            _existFilePath2 = Path.Combine(_existFolderPath1, "file2.txt");
            _existFilePath3 = Path.Combine(_existFolderPath2, "file3.txt");
            _existFilePath4 = Path.Combine(_existFolderPath2, "file4.txt");
            _noExistFilePath = Path.Combine(_existFolderPath2, "file5.txt");

            Directory.CreateDirectory(root);
            Directory.CreateDirectory(Path.Combine(root, _existFolderPath1));
            Directory.CreateDirectory(Path.Combine(root, _existFolderPath2));

            File.Create(Path.Combine(root, _existFilePath1));
            File.Create(Path.Combine(root, _existFilePath2));
            File.Create(Path.Combine(root, _existFilePath3));
            File.Create(Path.Combine(root, _existFilePath4));

            service = new FileStorageService(root);
        }

        [TestMethod]
        public void CopyFileTest()
        {
            throw new NotImplementedException();
            //service.CopyFile();
        }

        [TestMethod]
        public void CreateFolderTest()
        {
            throw new NotImplementedException();
            //service.CreateFolder();
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteFolderTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ExistsFileTest()
        {
            Assert.IsTrue(service.ExistsFile(_existFilePath1));
            Assert.IsFalse(service.ExistsFile(_noExistFilePath));
        }

        [TestMethod]
        public void ExistsFolderTest()
        {
            Assert.IsTrue(service.ExistsFolder(_existFolderPath1));
            Assert.IsFalse(service.ExistsFolder(_noExistFolderPath));
        }

        [TestMethod]
        public void GetFileTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetFileInfoTest()
        {
            var fileInfo = service.GetFileInfo(_existFilePath1);
            Assert.AreEqual(Path.GetFileName(_existFilePath1), fileInfo.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetFileInfoNotFoundExceptionTest()
        {
            var fileInfo = service.GetFileInfo(_noExistFilePath);
        }

        [TestMethod]
        public void GetFilesCountTest()
        {
            Assert.AreEqual(
                new DirectoryInfo(_existFolderPath1).GetFiles().Length, 
                service.GetFiles(_existFolderPath1).Count());
        }

        [TestMethod]
        public void GetFolderInfoTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetFoldersCountTest()
        {
            Assert.AreEqual(
                new DirectoryInfo(root).GetDirectories().Length,
                service.GetFolders(String.Empty).Count());
        }

        [TestMethod]
        public void MoveFileTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void WriteFileTest()
        {
            throw new NotImplementedException();
        }
    }
}
