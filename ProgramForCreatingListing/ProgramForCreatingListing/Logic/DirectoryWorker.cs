using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProgramForCreatingListing
{
    class DirectoryWorker
    {
        public delegate void DLoading();
        public static event DLoading onSearchFiles;

        public static List<string> AnalysisDirectory()
        {
            FolderBrowserDialog dialogForChoseDirectory = new FolderBrowserDialog();
            if (dialogForChoseDirectory.ShowDialog() == DialogResult.OK)
            {
                //Поставить ниже проверку на исключение, если пользоватль отказался от диалога. Возможно уже не надо
                string pathToDirectory = dialogForChoseDirectory.SelectedPath.ToString();
                return SearchingAllFiles(pathToDirectory);
            }
            
            return new List<string>();
        }

        static List<string> SearchingAllFiles(string pathToDirectory)
        {
            List<string> pathsToFiles = new List<string>();
            
            try
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(pathToDirectory);
                
                foreach (var catalog in currentDirectory.GetDirectories())
                {
                    pathsToFiles.AddRange(SearchingAllFiles(catalog.FullName));
                }

                foreach (var file in currentDirectory.GetFiles())
                {
                    pathsToFiles.Add(file.FullName);
                }
                onSearchFiles();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string> { };
            }
            
            return pathsToFiles;
        }

        public static List<string> AnalysisDirectory(string typeOfFiles)
        {
            FolderBrowserDialog dialogForChoseDirectory = new FolderBrowserDialog();
            dialogForChoseDirectory.ShowDialog();
            //Поставить ниже проверку на исключение, если пользоватль отказался от диалога. Возможно уже не надо
            string pathToDirectory = dialogForChoseDirectory.SelectedPath.ToString();

            if (pathToDirectory == "")
            {
                return new List<string> { };
            }
            return SearchingAllFiles(pathToDirectory, typeOfFiles);
        }

        static List<string> SearchingAllFiles(string pathToDirectory, string typeOfFiles)
        {
            List<string> pathsToFiles = new List<string>();
            //onSearchFiles();
            DirectoryInfo currentDirectory = new DirectoryInfo(pathToDirectory);
            foreach (var catalog in currentDirectory.GetDirectories())
            {
                pathsToFiles.AddRange(SearchingAllFiles(catalog.FullName, typeOfFiles));
            }

            foreach (var file in currentDirectory.GetFiles())
            {
                if (file.Extension == typeOfFiles)
                    pathsToFiles.Add(file.FullName);
            }

            return pathsToFiles;
        }
    }
}
