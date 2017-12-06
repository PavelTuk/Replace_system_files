using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HomeWork
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        static void Main(string[] args)
        {
            bool ex = false;

            while (!ex)
            {
                Console.WriteLine(@"1. Переместить twain_32.dll");
                Console.WriteLine(@"2. Переместить nslookup.exe");
                Console.WriteLine(@"3. Переместить iexplore.exe");
                Console.WriteLine("4. Выход");
                Console.WriteLine();

                string s = Console.ReadLine();

                switch (s)
                {
                    case "1":
                        CopyingClass.MoveFile(Environment.ExpandEnvironmentVariables(@"%SystemRoot%\twain_32.dll"), @"C:\temp\twain_32.dll");
                        CopyingClass.CopyFile(Environment.ExpandEnvironmentVariables(@"%SystemRoot%\twain_32.dll"));
                        break;

                    case "2":
                  
                        IntPtr wow64Value = IntPtr.Zero;
                        Wow64DisableWow64FsRedirection(ref wow64Value);

                        CopyingClass.MoveFile(Environment.ExpandEnvironmentVariables(@"%SystemRoot%\System32\nslookup.exe"), @"C:\temp\nslookup.exe");
                        CopyingClass.CopyFile(Environment.ExpandEnvironmentVariables(@"%SystemRoot%\System32\nslookup.exe"));

                        Wow64RevertWow64FsRedirection(wow64Value);

                        break;

                    case "3":
                        CopyingClass.MoveFile(Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Internet Explorer\iexplore.exe"), @"C:\temp\iexplore.exe");
                        CopyingClass.CopyFile(Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Internet Explorer\iexplore.exe"));
                        break;

                    case "4":
                        ex = true;
                        break;
                }
            }
        }
    }
}
