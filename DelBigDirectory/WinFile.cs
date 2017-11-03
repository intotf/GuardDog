using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DelBigDirectory
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

        private class FileFinder
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
                this.GetEnumerator();
            }

            public void GetEnumerator()
            {

                IntPtr value = new IntPtr(-1);
                WinFile.WIN32_FIND_DATA data;
                IntPtr intPtr = WinFile.FileFinder.FindFirstFile(this.fileFilter, out data);

                if (!(intPtr == value))
                {
                    if (data.dwFileAttributes != FileAttributes.Directory)
                    {
                        new WinFile(dir, data);
                        return;
                    }
                    while (WinFile.FileFinder.FindNextFile(intPtr, out data))
                    {
                        Deleter.DelFile(data.cFileName);
                    }
                    WinFile.FileFinder.FindClose(intPtr);
                }
                //yield break;
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

        public static void SearchByDel(string dir, string filter = "*.*")
        {
            new WinFile.FileFinder(dir, filter);
        }

        public override string ToString()
        {
            return this.FileName;
        }
    }
}
