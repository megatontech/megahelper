#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils
{
    public class FileHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FileHelper()
        {
        }

        #region Public members

        /// <summary>
        /// 判断是否为unc路径
        /// Returns whether the path is a UNC path.
        /// </summary>
        /// <param name="path">The path string.</param>
        /// <returns><c>true</c> if the path is a UNC path.</returns>
        public static bool IsUncPath(string path)
        {
            //  FIRST, check if this is a URL or a UNC path; do this by attempting to construct uri object from it
            Uri url = new Uri(path);

            if (url.IsUnc)
            {
                //  it is a unc path, return true
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 拼接unc路径
        /// Takes a UNC or URL path, determines which it is (NOT hardened against bad strings, assumes one or the other is present)
        /// and returns the path with correct trailing slash: backslash for UNC or
        /// slash mark for URL.
        /// </summary>
        /// <param name="path">The URL or UNC string.</param>
        /// <returns>Path with correct terminal slash.</returns>
        public static string AppendSlashUrlOrUnc(string path)
        {
            if (IsUncPath(path))
            {
                //  it is a unc path, so decorate the end with a back-slash (to correct misconfigurations, defend against trivial errors)
                return AppendTerminalBackslash(path);
            }
            else
            {
                //  assume URL here
                return AppendTerminalForwardSlash(path);
            }
        }

        /// <summary>
        /// 给文件夹地址加反斜杠\结束
        /// Path.DirectorySeparatorChar=\
        /// If not present appends terminal backslash to paths.
        /// </summary>
        /// <param name="path">A path string; for example, "C:\AppUpdaterClient".</param>
        /// <returns>A path string with trailing backslash; for example, "C:\AppUpdaterClient\".</returns>
        public static string AppendTerminalBackslash(string path)
        {
            if (path.IndexOf(Path.DirectorySeparatorChar, path.Length - 1) == -1)
            {
                return path + Path.DirectorySeparatorChar;
            }
            else
            {
                return path;
            }
        }

        /// <summary>
        /// 给文件夹地址加斜杠/结束
        /// Path.AltDirectorySeparatorChar=/
        /// Appends a terminal slash mark if there is not already one; returns corrected path.
        /// </summary>
        /// <param name="path">The path that may be missing a terminal slash mark.</param>
        /// <returns>The corrected path with terminal slash mark.</returns>
        public static string AppendTerminalForwardSlash(string path)
        {
            if (path.IndexOf(Path.AltDirectorySeparatorChar, path.Length - 1) == -1)
            {
                return path + Path.AltDirectorySeparatorChar;
            }
            else
            {
                return path;
            }
        }

        /// <summary>
        /// 系统临时目录下添加临时文件guid命名
        /// Creates a new temporary folder under the system temp folder
        /// and returns its full pathname.
        /// </summary>
        /// <returns>The full temp path string.</returns>
        public static string CreateTemporaryFolder()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
        }

        /// <summary>
        /// 复制并覆盖文件
        /// Copies files from the source to destination directories. Directory.Move is not
        /// suitable here because the downloader may still have the temporary
        /// directory locked.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        public static void CopyDirectory(string sourcePath, string destinationPath)
        {
            CopyDirectory(sourcePath, destinationPath, true);
        }

        /// <summary>
        /// 复制文件，可选覆盖
        /// Copies files from the source to destination directories. Directory.Move is not
        /// suitable here because the downloader may still have the temporary
        /// directory locked.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="overwrite">Indicates whether the destination files should be overwritten.</param>
        public static void CopyDirectory(string sourcePath, string destinationPath, bool overwrite)
        {
            CopyDirRecurse(sourcePath, destinationPath, destinationPath, overwrite);
        }

        /// <summary>
        /// 移动文件并重新命名
        /// Move a file from a folder to a new one.
        /// </summary>
        /// <param name="existingFileName">The original file name.</param>
        /// <param name="newFileName">The new file name.</param>
        /// <param name="flags">Flags about how to move the files.</param>
        /// <returns>indicates whether the file was moved.</returns>
        public static bool MoveFile(string existingFileName, string newFileName, MoveFileFlag flags)
        {
            return MoveFileEx(existingFileName, newFileName, (int)flags);
        }

        /// <summary>
        /// 删除目录及子目录
        /// Deletes a folder. If the folder cannot be deleted at the time this method is called,
        /// the deletion operation is delayed until the next system boot.
        /// </summary>
        /// <param name="folderPath">The directory to be removed</param>
        public static void DestroyFolder(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                }
            }
            catch (Exception)
            {
                // If we couldn't remove the files, postpone it to the next system reboot
                if (Directory.Exists(folderPath))
                {
                    FileHelper.MoveFile(
                        folderPath,
                        null,
                        MoveFileFlag.DelayUntilReboot);
                }
            }
        }

        /// <summary>
        /// 删除文件，如果不能删除就下次启动时删除
        /// Deletes a file. If the file cannot be deleted at the time this method is called,
        /// the deletion operation is delayed until the next system boot.
        /// </summary>
        /// <param name="filePath">The file to be removed</param>
        public static void DestroyFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                if (File.Exists(filePath))
                {
                    FileHelper.MoveFile(
                        filePath,
                        null,
                        MoveFileFlag.DelayUntilReboot);
                }
            }
        }

        /// <summary>
        /// 清空目录
        /// Clear up a folder. Delete all sub folders and files in the folder.
        /// </summary>
        /// <param name="folderPath">The directory to be cleared up</param>
        public static void ClearUpFolder(string folderPath)
        {
            // Delete all sub folders
            string[] dirs = Directory.GetDirectories(folderPath);
            for (int i = 0; i < dirs.Length; i++)
            {
                DestroyFolder(dirs[i]);
            }

            // Delete all files
            string[] files = Directory.GetFiles(folderPath);
            for (int i = 0; i < files.Length; i++)
            {
                DestroyFile(files[i]);
            }
        }

        /// <summary>
        /// 获取运行库版本
        /// Returns the path to the newer version of the .NET Framework installed on the system.
        /// </summary>
        /// <returns>A string containig the full path to the newer .Net Framework location</returns>
        public static string GetLatestDotNetFrameworkPath()
        {
            Version latestVersion = null;
            string fwkPath = Path.GetFullPath(Path.Combine(Environment.SystemDirectory, @"..\Microsoft.NET\Framework"));
            foreach (string path in Directory.GetDirectories(fwkPath, "v*"))
            {
                string candidateVersion = Path.GetFileName(path).TrimStart('v');
                try
                {
                    Version curVersion = new Version(candidateVersion);
                    if (latestVersion == null || (latestVersion != null && latestVersion < curVersion))
                    {
                        latestVersion = curVersion;
                    }
                }
                catch { }
            }

            return Path.Combine(fwkPath, "v" + latestVersion.ToString());
        }

        public void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        public Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #endregion Public members

        #region Private members

        /// <summary>
        /// API declaration of the Win32 function.
        /// </summary>
        /// <param name="lpExistingFileName">Existing file path.</param>
        /// <param name="lpNewFileName">The file path.</param>
        /// <param name="dwFlags">Move file flags.</param>
        /// <returns>Whether the file was moved or not.</returns>
        [DllImport("KERNEL32.DLL")]
        private static extern bool MoveFileEx(

            string lpExistingFileName,
            string lpNewFileName,
            long dwFlags);

        /// <summary>
        /// Utility function that recursively copies directories and files.
        /// Again, we could use Directory.Move but we need to preserve the original.
        /// </summary>
        /// <param name="sourcePath">The source path to copy.</param>
        /// <param name="destinationPath">The destination path to copy to.</param>
        /// <param name="originalDestination">The original dstination path.</param>
        /// <param name="overwrite">Whether the folders should be copied recursively.</param>
        private static void CopyDirRecurse(string sourcePath, string destinationPath, string originalDestination, bool overwrite)
        {
            //  ensure terminal backslash
            sourcePath = FileHelper.AppendTerminalBackslash(sourcePath);
            destinationPath = FileHelper.AppendTerminalBackslash(destinationPath);

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            //  get dir info which may be file or dir info object
            DirectoryInfo dirInfo = new DirectoryInfo(sourcePath);

            string destFileName = null;

            foreach (FileSystemInfo fsi in dirInfo.GetFileSystemInfos())
            {
                if (fsi is FileInfo)
                {
                    destFileName = Path.Combine(destinationPath, fsi.Name);

                    //  if file object just copy when overwrite is allowed
                    if (File.Exists(destFileName))
                    {
                        if (overwrite)
                        {
                            File.Copy(fsi.FullName, destFileName, true);
                        }
                    }
                    else
                    {
                        File.Copy(fsi.FullName, destFileName);
                    }
                }
                else
                {
                    // avoid this recursion path, otherwise copying directories as child directories
                    // would be an endless recursion (up to an stack-overflow exception).
                    if (fsi.FullName != originalDestination)
                    {
                        //  must be a directory, create destination sub-folder and recurse to copy files
                        //Directory.CreateDirectory( destinationPath + fsi.Name );
                        CopyDirRecurse(fsi.FullName, destinationPath + fsi.Name, originalDestination, overwrite);
                    }
                }
            }
        }

        #endregion Private members
    }

    /// <summary>
    /// Indicates how to proceed with the move file operation.
    /// </summary>
    [Flags]
    public enum MoveFileFlag : int
    {
        /**/
        /// <summary>

        /// Perform a default move funtion.
        /// </summary>
        None = 0x00000000,

        /**/
        /// <summary>

        /// If the target file exists, the move function will replace it.
        /// </summary>
        ReplaceExisting = 0x00000001,

        /**/
        /// <summary>

        /// If the file is to be moved to a different volume,
        /// the function simulates the move by using the CopyFile and DeleteFile functions.
        /// </summary>
        CopyAllowed = 0x00000002,

        /**/
        /// <summary>

        /// The system does not move the file until the operating system is restarted.
        /// The system moves the file immediately after AUTOCHK is executed, but before
        /// creating any paging files. Consequently, this parameter enables the function
        /// to delete paging files from previous startups.
        /// </summary>
        DelayUntilReboot = 0x00000004,

        /**/
        /// <summary>

        /// The function does not return until the file has actually been moved on the disk.
        /// </summary>
        WriteThrough = 0x00000008,

        /**/
        /// <summary>

        /// Reserved for future use.
        /// </summary>
        CreateHardLink = 0x00000010,

        /**/
        /// <summary>

        /// The function fails if the source file is a link source, but the file cannot be tracked after the move. This situation can occur if the destination is a volume formatted with the FAT file system.
        /// </summary>
        FailIfNotTrackable = 0x00000020,
    }
}