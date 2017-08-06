using System;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace ProgramForCreatingListing
{
    public static class FileWorker
    {
        public static string ReadFromFile(string pathToFile)
        {
            string timestr = "";
            try
            {
                StreamReader ReadTxt = new StreamReader(pathToFile, Encoding.GetEncoding(1251));
                timestr = ReadTxt.ReadToEnd().ToString();
                ReadTxt.Close();
            }
            //Проверить, какое исключение может вылетать
            catch (Exception ex)
            {
            }

            return timestr;
        }

        public static bool WriteToFile(string data)
        {
            //dialogForWriting.Filter = "Текстовые файлы (*.txt)|*.txt";
            ////Что-нибудь придумать с начальной директорией    
            ////dialog.InitialDirectory = InitialDirectory;/*"C:"*/;
            //dialogForWriting.CreatePrompt = true;
            //dialogForWriting.OverwritePrompt = false;
            //dialogForWriting.DefaultExt = ".txt";
            //dialogForWriting.ShowDialog();
            //Проверить на исклбчения. Они появляются, когда выбрал файлы и собираешься их записать
            // А потом отменяешь это дело
            try
            {
                StreamWriter WriteText = new StreamWriter(ChoosePathToFile(), true);
                WriteText.Write(data);
                WriteText.Close();
            }
            catch(ArgumentException)
            {
                return false;
            }
            return true;
        }

        public static string ChoosePathToFile()
        {
            OpenFileDialog dialogForChoseFile = new OpenFileDialog();
            dialogForChoseFile.ShowDialog();
            return dialogForChoseFile.FileName;
        }
    }
}
