using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;


namespace ProgramForCreatingListing.Logic
{
    public class WordWorker
    {
        //Проверить потом, что будет, если убрать нижнее подчеркивание перед наименованием
        //интерфейсов, т.е. Word.Application
        Word._Application application; 
        Word._Document document;

        Object missingObj = System.Reflection.Missing.Value;
        Object trueObj = true;
        Object falseObj = false;

        public void RunWord()
        {
            //создаем обьект приложения word
            application = new Word.Application();
            // создаем путь к файлу
            //Object templatePathObj = FileWorker.ChoosePathToFile(); 
            // если вылетим не этом этапе, приложение останется открытым
            try
            {
                document = application.Documents.Add();
                // Метод ниже нужен для создания НОВОГО документа на основе шаблона старого.
                // Add(ref  templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                document.Close(ref falseObj, ref  missingObj, ref missingObj);
                application.Quit(ref missingObj, ref  missingObj, ref missingObj);
                document = null;
                application = null;
                throw error;
            }
            application.Visible = true;

        }

        public void InsertTextIntoWord(string data)
        {
            object start = 0;
            object end = 0;
            Word.Range currentRange = document.Range(ref start, ref end);
            //currentRange.ParagraphFormat.Borders.DistanceFromBottom = 0;
            //currentRange.ParagraphFormat.Borders.DistanceFromLeft = 0;
            //currentRange.ParagraphFormat.Borders.DistanceFromTop = 0;
            //currentRange.ParagraphFormat.Borders.JoinBorders = true;
            //currentRange.Paragraphs.LineSpacing = currentRange.Font.Size + 2;
            //currentRange.


            currentRange.Text = data;
            currentRange.Font.Size = 11;
            currentRange.Font.Name = "Times New Roman";
            currentRange.Paragraphs.Space1();
            currentRange.Paragraphs.SpaceAfter = 0;
            currentRange.Paragraphs.SpaceBefore = 0;
            //document.Content.Select();
            //currentRange.
            //currentRange.Borders.DistanceFromBottom = 0;
            //currentRange.Borders.DistanceFromTop = 0;
            //currentRange.Borders.DistanceFromLeft = 0;
            //currentRange.Cut();


            //object start1 = document.Content.Start;
            //object end1 = document.Content.End;
            //Word.Range currentRange1 = document.Range(ref start1, ref end1);
            //currentRange1.PageSetup.BottomMargin = 30;
            //currentRange1.
            //currentRange1.Cut();
            //currentRange1.Borders.DistanceFromBottom = 0;
            //currentRange1.Borders.DistanceFromTop = 0;
            //currentRange1.Borders.DistanceFromLeft = 0;
            //currentRange.ParagraphFormat.Borders.DistanceFromBottom = 0;
            //currentRange.ParagraphFormat.Borders.DistanceFromLeft = 0;
            //currentRange.ParagraphFormat.Borders.DistanceFromTop = 0;

            //application.Documents.Save();
            //application.Quit();

        }
    }
}
