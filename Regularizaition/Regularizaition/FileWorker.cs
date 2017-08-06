using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Regularizaition
{
    /// <summary>
    /// Класс работы с файлами.
    /// </summary>
    public static class FileWorker
    {
        /// <summary>
        /// Директория файла.
        /// </summary>
        static public string InitialDirectory;

        /// <summary>
        /// Считывание файла.
        /// </summary>
        /// <returns>Содержимое файла(строка).</returns>
        public static string ReadFromFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                string timestr = "";
                try
                {
                    //Установка параметров.
                    dialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                    dialog.InitialDirectory = InitialDirectory;/*"C:"*/;
                    dialog.DefaultExt = ".txt";
                    dialog.ShowDialog();
                    //Считывание файла.
                    StreamReader ReadThisTxt = new StreamReader(dialog.FileName);
                    timestr = ReadThisTxt.ReadToEnd().ToString();
                    ReadThisTxt.Close();
                }
                catch (Exception ex)
                {
                }
                return timestr;
            }
        }

        /// <summary>
        /// Запись в файл.
        /// </summary>
        /// <param name="saveme">Сохраняемый результат(строка).</param>
        public static void WriteToFile(string saveme)
        {
            using (var dialog = new SaveFileDialog())
            {
                //Установка параметров.
                dialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                dialog.InitialDirectory = InitialDirectory;/*"C:"*/;
                dialog.CreatePrompt = true;
                dialog.OverwritePrompt = false;
                dialog.DefaultExt = ".txt";
                dialog.ShowDialog();
                if (dialog.FileName != "")
                {
                    using (var File = new StreamWriter(dialog.FileName.ToString(), true))
                    {
                        //Запись файла.
                        File.Write(saveme);
                        File.Close();
                    }
                }
            }
        }

    }
}
