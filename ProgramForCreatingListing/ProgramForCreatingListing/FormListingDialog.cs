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
        ButtonOpenFile buttonOpenFile;
        InformationLabel informationLabel;

        List<int> choosenElementsUnsort = new List<int>();

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
                case "buttonOpenFile":
                    buttonOpenFile = (ButtonOpenFile)component;
                    break;
                case "informationLabel":
                    informationLabel = (InformationLabel)component;
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

            //Сколько вызовов сделается, столько откликов на событие и создатся!!!
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
            progressBarLoadingFiles.Value = progressBarLoadingFiles.Maximum;
            DirectoryWorker.onSearchFiles -= Loading;

        }

        public void Loading(int numbOfDirectories)
        {
            //При выборе папки Дмитрий 33 возникает исключение
            progressBarLoadingFiles.Maximum = numbOfDirectories + 1;
            //try
            //{
            ++progressBarLoadingFiles.Value;
            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    progressBarLoadingFiles.Maximum = progressBarLoadingFiles.Value + 1;
            //    ++progressBarLoadingFiles.Value;
            //}


        }

        public void FormListing()
        {
            //string[] items = new string[listOfFiles.CheckedIndices.Count];
            string[] items = new string[choosenElementsUnsort.Count];
            //for (int i = 0; i < items.Length; ++i)
            //{
            //    //items[i] = listOfFiles.CheckedItems[i].ToString();
            //}

            int counter = 0;
            foreach (int unsort_i in choosenElementsUnsort)
            {
                items[counter] = listOfFiles.Items[unsort_i].ToString();
                counter++;
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
                        if (choosenElementsUnsort.Contains(i))
                            choosenElementsUnsort.Remove(i);
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
            if (listOfFiles.SelectedIndex != -1)
            {
                if (listOfFiles.GetItemCheckState(listOfFiles.SelectedIndex) == CheckState.Checked)
                {
                    choosenElementsUnsort.Add(listOfFiles.SelectedIndex);
                }
                else
                    if (choosenElementsUnsort.Contains(listOfFiles.SelectedIndex))
                        choosenElementsUnsort.Remove(listOfFiles.SelectedIndex);
            }

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

        public void OpenFile()
        {
            if (listOfFiles.SelectedIndex != -1)
                ActionLogic.OpenSelectedFileInNotepad((string)listOfFiles.Items[listOfFiles.SelectedIndex]);
        }

        public void ChooseTxtFormatListing()
        {
        }

        public void ChooseWordFormatListing()
        {
        }
    }
}
