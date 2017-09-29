using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ProgramForCreatingListing
{
    public class ActionLogic
    {
        static void selectAllElements(bool flag)
        {
            
        }
        
        public static string FormDataString(string[] pathsOfFiles)
        {
            string data = "";
            for(int i = 0; i < pathsOfFiles.Length; ++i) 
            {
                data += FileWorker.ReadFromFile(pathsOfFiles[i]) + "\r\n";
            }
            return data;
        }

        public static string FormDataString(string[] pathsOfFiles, string delimiter)
        {
            bool isDelimiterFileName = false;
            switch (delimiter)
            {
                case "*Название файла*":
                    isDelimiterFileName = true;
                    break;
            }

            string data = "";
            if (isDelimiterFileName)
            {
                for (int i = 0; i < pathsOfFiles.Length; ++i)
                {
                    delimiter = GetFileNameFromPath(pathsOfFiles[i]);
                    data += delimiter + "\r\n" + "\r\n";
                    data += FileWorker.ReadFromFile(pathsOfFiles[i]) + "\r\n" + "\r\n";
                }
            }
            else
            {
                for (int i = 0; i < pathsOfFiles.Length; ++i)
                {
                    data += delimiter + "\r\n";
                    data += FileWorker.ReadFromFile(pathsOfFiles[i]) + "\r\n";
                }
            }
            
            return data;
        }

        public static string GetFileNameFromPath(string path)
        {
            string fileName = "";
            for (int i = path.LastIndexOf('\\') + 1; i < path.Length; ++i)
            {
                fileName += path[i];
            }
            return fileName;
        }

        public static void OpenSelectedFileInNotepad(string pathToFile)
        {
            Process.Start("notepad++.exe", pathToFile);
        }
        //public static string FormStringWithDelimiter(string[] pathsOfFiles, string delimiter)
        //{
        //    string str = "";
        //    for (int i = 0; i < pathsOfFiles.Length; ++i)
        //    {
        //        str += delimiter + "\r\n";
        //        str += FileWorker.ReadFromFile(pathsOfFiles[i]) + "\r\n";
        //    }
        //    return str;
        //}
    }
}
