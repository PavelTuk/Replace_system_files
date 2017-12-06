using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace HomeWork
{
    public class CopyingClass
    {
        public static void AddFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            FileSecurity fSecurity = File.GetAccessControl(fileName);
            fSecurity.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));
            File.SetAccessControl(fileName, fSecurity);
        }

        public static void MoveFile(string path, string path2)
        {
            if (File.Exists(path))
            {
                try
                {
                    #region Права
                    UnmanagedCode.GiveRestorePrivilege();

                    WindowsIdentity wi = WindowsIdentity.GetCurrent();
                    string user = wi.Name;

                    FileSecurity fSecurity = File.GetAccessControl(path);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(@user, FileSystemRights.FullControl, AccessControlType.Allow));
                    File.SetAccessControl(path, fSecurity);
                    #endregion

                    Directory.CreateDirectory(path2.Substring(0, path2.LastIndexOf('\\'))); //создание папки, если она отсутствует

                    if (File.Exists(path2)) //удаление файла, если он существует в папке назначения
                        File.Delete(path2);

                    File.Move(path, path2); //перемещение исходного файла
                    Console.WriteLine("{0} был перемещен в {1}.", path, path2);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
        }

        public static void CopyFile(string path)
        {
            if (File.Exists(path))
            {
                #region Права
                try
                {
                    WindowsIdentity wi = WindowsIdentity.GetCurrent();
                    string user = wi.Name;

                    FileSecurity fSecurity = File.GetAccessControl(path);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(@user, FileSystemRights.FullControl, AccessControlType.Allow));
                    File.SetAccessControl(path, fSecurity);
                }
               
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                }
                #endregion

                File.Delete(path);
            }

            try
            {         
                File.Copy(System.Reflection.Assembly.GetExecutingAssembly().Location, path); //создание копии программы с именем исходного файла 
                Console.WriteLine("Копия создана в {0}.", path);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
