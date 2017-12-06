using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.InteropServices;

namespace HomeWork.Tests
{
    [TestClass]
    public class CopyingClassTests
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [TestMethod]
        public void move_file_to_temp_folder() // Переместить исходный файл
        {
            //arrange
            string path_1 = @"%SystemRoot%\twain_32.dll"; // Переместить twain_32.dll
            string path_2 = @"C:\temp\twain_32.dll";

            //string path_1 = @"%ProgramFiles%\Internet Explorer\iexplore.exe"; // Переместить iexplore.exe
            //string path_2 = @"C:\temp\iexplore.exe";

            //act            
            CopyingClass.MoveFile(Environment.ExpandEnvironmentVariables(path_1), path_2);

            //assert
            Assert.IsTrue(File.Exists(path_2));
        }

        [TestMethod]
        public void copy_app_to_source_folder() // Создать копию приложения
        {
            //arrange
            string path = @"%SystemRoot%\twain_32.dll";
            //string path = @"%ProgramFiles%\Internet Explorer\iexplore.exe";

            //act            
            CopyingClass.CopyFile(Environment.ExpandEnvironmentVariables(path));

            //assert
            Assert.IsTrue(File.Exists(Environment.ExpandEnvironmentVariables(path)));
        }


        //Следующие два теста сделаны отдельно, так как для доступа к файлу nslookup.exe нужно отключить перенаправление // File System Redirector

        [TestMethod]
        public void move_nslookup_exe_to_temp_folder() // Переместить nslookup.exe
        {
            //arrange
            string path_1 = @"%SystemRoot%\System32\nslookup.exe";
            string path_2 = @"C:\temp\nslookup.exe";

            //act            
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);

            CopyingClass.MoveFile(Environment.ExpandEnvironmentVariables(path_1), path_2);

            //assert
            Assert.IsTrue(File.Exists(path_2));

            Wow64RevertWow64FsRedirection(wow64Value);
        }

        [TestMethod]
        public void copy_app_to_nslookup_folder() // Создать копию приложения
        {
            //arrange
            string path = @"%SystemRoot%\System32\nslookup.exe";

            //act    
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);

            CopyingClass.CopyFile(Environment.ExpandEnvironmentVariables(path));

            //assert
            Assert.IsTrue(File.Exists(Environment.ExpandEnvironmentVariables(path)));
            Wow64RevertWow64FsRedirection(wow64Value);
        }
    }
}
