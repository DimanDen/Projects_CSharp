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
        public delegate void DLoading(int numbOfDirectories);
        public static event DLoading onSearchFiles;

        private static int numbOfDirectories = 0;

        static int SearchNumbOfElementsInCatalog(string pathToDirectory)
        {
            int numbOfElements = 0;
            try
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(pathToDirectory);
                //numbOfElements = currentDirectory.GetDirectories().Length;

                foreach (var catalog in currentDirectory.GetDirectories())
                {
                    numbOfElements++;
                    numbOfElements += SearchNumbOfElementsInCatalog(catalog.FullName);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return 0;
            }

            return numbOfElements;
        }

        public static List<string> AnalysisDirectory(string typeOfFiles)
        {
            numbOfDirectories = 0;
            FolderBrowserDialog dialogForChoseDirectory = new FolderBrowserDialog();
            if (dialogForChoseDirectory.ShowDialog() == DialogResult.OK)
            {
                //Поставить ниже проверку на исключение, если пользоватль отказался от диалога. Возможно уже не надо
                string pathToDirectory = dialogForChoseDirectory.SelectedPath.ToString();
                numbOfDirectories = SearchNumbOfElementsInCatalog(pathToDirectory);
                return SearchingAllFiles(pathToDirectory, typeOfFiles);
            }
            return new List<string> { };
        }

        public static List<string> AnalysisDirectory()
        {
            numbOfDirectories = 0;
            FolderBrowserDialog dialogForChoseDirectory = new FolderBrowserDialog();
            if (dialogForChoseDirectory.ShowDialog() == DialogResult.OK)
            {
                //Поставить ниже проверку на исключение, если пользоватль отказался от диалога. Возможно уже не надо
                string pathToDirectory = dialogForChoseDirectory.SelectedPath.ToString();
                numbOfDirectories = SearchNumbOfElementsInCatalog(pathToDirectory);
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

            }
            catch (UnauthorizedAccessException)
            {
                return new List<string> { };
            }
            onSearchFiles(numbOfDirectories);
            return pathsToFiles;
        }

        static List<string> SearchingAllFiles(string pathToDirectory, string typeOfFiles)
        {
            List<string> pathsToFiles = new List<string>();
            try
            {
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
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string> { };
            }
            onSearchFiles(numbOfDirectories);
            return pathsToFiles;
        }
    }
}
