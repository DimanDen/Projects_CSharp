using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramForCreatingListing.Components;
using System.Windows.Forms;
using ProgramForCreatingListing.Logic;
using System.Threading;

namespace ProgramForCreatingListing
{
    public class FormListingDialog : IMediator
    {
        ButtonSearch btnSearch;
        ButtonWrite btnWrite;
        TextBoxWithHint textBoxChooseType;
        ListOfFiles listOfFiles;
        ButtonDelete btnDelete;
        TextBoxDelimiterWithHint textBoxDelimiterBtwFiles;
        CheckBoxChooseAllElements checkBoxChooseAllElements;
        CheckBoxMsWordType checkBoxMsWordType;
        CheckBoxTxtType checkBoxTxtType;
        ProgressBarLoadingFiles progressBarLoadingFiles;

        public void RegisterComponent(IComponentExecute component)
        {
            component.SetMediator(this);
            //MessageBox.Show(component.GetName());
            switch (component.GetName())
            {
                case "buttonSearchFiles":
                    btnSearch = (ButtonSearch)component;
                    break;
                case "listOfFiles":
                    listOfFiles = (ListOfFiles)component;
                    break;
                case "textBoxChooseType":
                    textBoxChooseType = (TextBoxWithHint)component;
                    break;
                case "textBoxDelimiterBtwFiles":
                    textBoxDelimiterBtwFiles = (TextBoxDelimiterWithHint)component;
                    break;
                case "checkBoxChooseAllElements":
                    checkBoxChooseAllElements = (CheckBoxChooseAllElements)component;
                    break;
                case "checkBoxMsWordTypeListing":
                    checkBoxMsWordType = (CheckBoxMsWordType)component;
                    break;
                case "checkBoxTxtTypeListing":
                    checkBoxTxtType = (CheckBoxTxtType)component;
                    break;
                case "progressBarLoadingFiles":
                    progressBarLoadingFiles = (ProgressBarLoadingFiles)component;
                    break;

            }
        }

        public void Notify(string note)
        {

        }

        public void SearchFiles()
        {
            //progressBarLoadingFiles.MarqueeAnimationSpeed = 30;
            //progressBarLoadingFiles.Style = ProgressBarStyle.Marquee;

            progressBarLoadingFiles.Value = 0;

            DirectoryWorker.onSearchFiles += Loading;

            List<string> pathsToFiles = new List<string>();
            if (textBoxChooseType.Text == "")
                pathsToFiles = DirectoryWorker.AnalysisDirectory();

            else
                pathsToFiles = DirectoryWorker.AnalysisDirectory(textBoxChooseType.Text);

            foreach (string path in pathsToFiles)
            {
                listOfFiles.Items.Add(path);
            }
            progressBarLoadingFiles.Value = 100;
        }

        public void Loading()
        {
            if (progressBarLoadingFiles.Value == 50 ||
                progressBarLoadingFiles.Value == 75)
                Thread.Sleep(150);

            if (progressBarLoadingFiles.Value == 100)
            {
                progressBarLoadingFiles.Value = 0;
            }
            progressBarLoadingFiles.Value++;
        }

        public void FormListing()
        {
            string[] items = new string[listOfFiles.CheckedIndices.Count];
            for (int i = 0; i < items.Length; ++i)
            {
                items[i] = listOfFiles.CheckedItems[i].ToString();
            }

            if (checkBoxMsWordType.Checked)
            {
                WordWorker wordWorker = new WordWorker();
                wordWorker.RunWord();
                wordWorker.InsertTextIntoWord(ActionLogic.FormDataString(items, textBoxDelimiterBtwFiles.Text));
            }
            if (checkBoxTxtType.Checked)
            {
                bool result = false;

                if (textBoxDelimiterBtwFiles.Text != "")
                {
                    result = FileWorker.WriteToFile(ActionLogic.FormDataString(items, textBoxDelimiterBtwFiles.Text));
                }
                else
                {
                    result = FileWorker.WriteToFile(ActionLogic.FormDataString(items));
                }

                if (result)
                {
                    MessageBox.Show("Листинг успешно сформирован", "Успешно", MessageBoxButtons.OKCancel);
                }
                else
                {
                    MessageBox.Show("Листинг не сформирован", "Неуспешно", MessageBoxButtons.OKCancel);
                }
            }



        }

        public void DeleteElementsFromList()
        {
            var numbOfDelete = listOfFiles.CheckedIndices.Count;

            for (int j = 0; j < numbOfDelete; ++j)
            {
                for (int i = 0; i < listOfFiles.Items.Count; ++i)
                {
                    if (listOfFiles.CheckedIndices.Contains(i))
                    {
                        listOfFiles.Items.Remove(listOfFiles.Items[i]);
                        break;
                    }
                }
            }
            CheckStatus();
        }

        public void ChangePerfomanceTextBox()
        {
            textBoxDelimiterBtwFiles.Multiline = !textBoxDelimiterBtwFiles.Multiline;
            if (textBoxDelimiterBtwFiles.Multiline)
                textBoxDelimiterBtwFiles.Height = textBoxChooseType.Height * 2;
            else
                textBoxDelimiterBtwFiles.Height = textBoxChooseType.Height;
        }

        public void ChooseAllElement()
        {
            if (checkBoxChooseAllElements.CheckState == CheckState.Checked)
            {
                setCheckedForAllElements(true);
            }
            else if (checkBoxChooseAllElements.CheckState == CheckState.Unchecked)
            {
                setCheckedForAllElements(false);
            }
        }

        private void setCheckedForAllElements(bool flag)
        {
            for (int i = 0; i < listOfFiles.Items.Count; ++i)
            {
                listOfFiles.SetItemChecked(i, flag);
            }
        }

        public void CheckStatus()
        {
            if ((listOfFiles.CheckedIndices.Count != 0)
                && (listOfFiles.CheckedIndices.Count != listOfFiles.Items.Count))
            {
                checkBoxChooseAllElements.CheckState = CheckState.Indeterminate;
            }
            else if (listOfFiles.CheckedIndices.Count == 0)
            {
                checkBoxChooseAllElements.CheckState = CheckState.Unchecked;
            }
            else if (listOfFiles.CheckedIndices.Count == listOfFiles.Items.Count)
            {
                checkBoxChooseAllElements.CheckState = CheckState.Checked;
            }
        }

        public void ChooseTxtFormatListing()
        {
        }

        public void ChooseWordFormatListing()
        {
        }
    }
}
