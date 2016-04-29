using System;
using System.Collections.Generic;
using System.IO;

namespace TF.Data.Systems.IO
{
    public class FileStorageService : IStorageService
    {
        readonly string root;

        public FileStorageService(string root)
        {
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            this.root = root;
        }

        public void CopyFile(string pathStart, string pathEnd)
        {
            pathStart = Path.Combine(root, pathStart);
            pathEnd = Path.Combine(root, pathEnd);

            throw new NotImplementedException();
        }

        public void CreateFolder(string path)
        {
            path = Path.Combine(root, path);

            throw new NotImplementedException();
        }

        public void DeleteFile(string path)
        {
            path = Path.Combine(root, path);

            throw new NotImplementedException();
        }

        public void DeleteFolder(string path)
        {
            path = Path.Combine(root, path);

            throw new NotImplementedException();
        }

        public bool ExistsFile(string path)
        {
            path = Path.Combine(root, path);

            return File.Exists(Path.Combine(root, path));
        }

        public bool ExistsFolder(string path)
        {
            path = Path.Combine(root, path);

            return Directory.Exists(Path.Combine(root, path));
        }

        public byte[] GetFile(string path)
        {
            path = Path.Combine(root, path);

            if (!File.Exists(path))
                throw new FileNotFoundException();

            using (var stream = new StreamReader(path))
            {
                var ms = new MemoryStream();
                stream.BaseStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public IFileInfo GetFileInfo(string path)
        {
            path = Path.Combine(root, path);

            var file = new System.IO.FileInfo(path);

            if (!file.Exists)
                throw new FileNotFoundException();

            return new FileInfo { Name = file.Name };
        }

        public IEnumerable<IFileInfo> GetFiles(string path)
        {
            path = Path.Combine(root, path);

            foreach (var file in new DirectoryInfo(path).GetFiles())
            {
                yield return new FileInfo
                {
                    Name = file.Name
                };
            }
        }

        public IFolderInfo GetFolderInfo(string path)
        {
            path = Path.Combine(root, path);

            throw new NotImplementedException();
        }

        public IEnumerable<IFolderInfo> GetFolders(string path)
        {
            path = Path.Combine(root, path);

            foreach (var file in new DirectoryInfo(path).GetDirectories())
            {
                yield return new FolderInfo
                {
                    Name = file.Name
                };
            }
        }

        public void MoveFile(string pathStart, string pathEnd)
        {
            pathStart = Path.Combine(root, pathStart);
            pathEnd = Path.Combine(root, pathEnd);

            throw new NotImplementedException();
        }

        public void WriteFile(string path, byte[] content)
        {
            path = Path.Combine(root, path);
            File.WriteAllBytes(path, content);
        }
    }
}
