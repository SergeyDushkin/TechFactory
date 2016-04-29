using System.Collections.Generic;

namespace TF.Data.Systems.IO
{
    public interface IStorageService
    {
        byte[] GetFile(string path);
        IFileInfo GetFileInfo(string path);
        IFolderInfo GetFolderInfo(string path);

        IEnumerable<IFileInfo> GetFiles(string path);
        IEnumerable<IFolderInfo> GetFolders(string path);

        bool ExistsFolder(string path);
        bool ExistsFile(string path);

        void DeleteFile(string path);
        void DeleteFolder(string path);

        void CreateFolder(string path);
        void WriteFile(string path, byte[] content);

        void CopyFile(string pathStart, string pathEnd);
        void MoveFile(string pathStart, string pathEnd);
    }
}
