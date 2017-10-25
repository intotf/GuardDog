using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FilesDelete
{
    public class WinFile
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct WIN32_FIND_DATA
        {
            public FileAttributes dwFileAttributes;

            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;

            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;

            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;

            public uint nFileSizeHigh;

            public uint nFileSizeLow;

            public uint dwReserved0;

            public uint dwReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        private class FileFinder : IEnumerable<WinFile>, IEnumerable
        {
            private readonly string dir;

            private readonly string fileFilter;

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr FindFirstFile(string lpFileName, out WinFile.WIN32_FIND_DATA lpFindFileData);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern bool FindNextFile(IntPtr hFindFile, out WinFile.WIN32_FIND_DATA lpFindFileData);

            [DllImport("kernel32.dll")]
            private static extern bool FindClose(IntPtr hFindFile);

            public FileFinder(string dir, string filter)
            {
                if (string.IsNullOrEmpty(dir))
                {
                    this.dir = Path.GetFullPath(Directory.GetCurrentDirectory());
                }
                else
                {
                    this.dir = Path.GetFullPath(dir);
                }
                if (string.IsNullOrEmpty(filter))
                {
                    filter = "*.*";
                }
                this.fileFilter = Path.Combine(this.dir, filter);
            }

            public IEnumerator<WinFile> GetEnumerator()
            {

                IntPtr value = new IntPtr(-1);
                WinFile.WIN32_FIND_DATA data;
                IntPtr intPtr = WinFile.FileFinder.FindFirstFile(this.fileFilter, out data);

                if (!(intPtr == value))
                {
                    if (data.dwFileAttributes != FileAttributes.Directory)
                    {
                        yield return new WinFile(this.dir, data);
                    }
                    while (WinFile.FileFinder.FindNextFile(intPtr, out data))
                    {
                        //剔除掉 如：D;\file.. 这种根名称
                        if (data.cFileName.Substring(data.cFileName.Length - 2, 2) != "..")
                        {
                            yield return new WinFile(this.dir, data);
                        }
                        //if (data.dwFileAttributes != FileAttributes.Directory )
                        //{
                        //    yield return new WinFile(this.dir, data);
                        //}
                    }
                    WinFile.FileFinder.FindClose(intPtr);
                }
                yield break;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        private readonly string dir;

        private readonly WinFile.WIN32_FIND_DATA data;

        public FileAttributes Attributes
        {
            get
            {
                return this.data.dwFileAttributes;
            }
        }

        public string FileName
        {
            get
            {
                return Path.Combine(this.dir, this.data.cFileName);
            }
        }

        public string FileDir
        {
            get
            {
                return this.dir;
            }
        }

        public DateTime CreationTime
        {
            get
            {
                return WinFile.ToDateTime(this.data.ftCreationTime);
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
                return WinFile.ToDateTime(this.data.ftLastWriteTime);
            }
        }

        public DateTime AccessTime
        {
            get
            {
                return WinFile.ToDateTime(this.data.ftLastAccessTime);
            }
        }

        public long Length
        {
            get
            {
                return WinFile.ToLong(this.data.nFileSizeHigh, this.data.nFileSizeLow);
            }
        }

        private WinFile(string dir, WinFile.WIN32_FIND_DATA data)
        {
            this.dir = dir;
            this.data = data;
        }

        private static DateTime ToDateTime(System.Runtime.InteropServices.ComTypes.FILETIME time)
        {
            long fileTime = WinFile.ToLong((uint)time.dwHighDateTime, (uint)time.dwLowDateTime);
            return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
        }

        private static long ToLong(uint high, uint low)
        {
            long num = (long)((ulong)high);
            num <<= 32;
            return num | (long)((ulong)low);
        }

        public void Delete()
        {
            File.Delete(this.FileName);
        }

        public static IEnumerable<WinFile> GetFiles()
        {
            return WinFile.GetFiles(null);
        }

        public static IEnumerable<WinFile> GetFiles(string dir)
        {
            return WinFile.GetFiles(dir, null);
        }

        public static IEnumerable<WinFile> GetFiles(string dir, string filter)
        {
            return new WinFile.FileFinder(dir, filter);
        }

        public override string ToString()
        {
            return this.FileName;
        }
    }
}
