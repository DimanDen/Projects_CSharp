using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramForCreatingListing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FormListingDialog dialog = new FormListingDialog();
            dialog.RegisterComponent(textBoxChooseType);
            dialog.RegisterComponent(buttonSearchFiles);
            dialog.RegisterComponent(listOfFiles);
            dialog.RegisterComponent(buttonWrite);
            dialog.RegisterComponent(buttonDelete);
            dialog.RegisterComponent(textBoxDelimiterBtwFiles);
            dialog.RegisterComponent(checkBoxChooseAllElements);
            dialog.RegisterComponent(checkBoxTxtTypeListing);
            dialog.RegisterComponent(checkBoxMsWordTypeListing);
            dialog.RegisterComponent(textBoxDelimiterBtwFiles);
            dialog.RegisterComponent(progressBarLoadingFiles);
            dialog.RegisterComponent(buttonOpenFile);
            dialog.RegisterComponent(informationLabel);
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            mainTabControl.Height = this.Height;
            mainTabControl.Width = this.Width;
        }

        private void textBoxChooseType_TextChanged(object sender, EventArgs e)
        {
            //textBoxChooseType.Execute();
        }

        private void buttonSearchFiles_Click(object sender, EventArgs e)
        {
            buttonSearchFiles.Execute();
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            buttonWrite.Execute();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //backgroundWorker1.RunWorkerAsync();
            buttonDelete.Execute();
            this.Refresh();
        }

        private void checkBoxChooseAllElements_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxChooseAllElements.Execute();
            this.Refresh();
        }

        private void textBoxDelimiterBtwFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //textBoxDelimiterBtwFiles.Execute();
            //this.Refresh();
        }

        private void listOfFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            listOfFiles.Execute();
            this.Refresh();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            buttonOpenFile.Execute();
        }

        private void mainTabPage1_Click(object sender, EventArgs e)
        {

        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    for (int i = 1; i <= 100; i++)
        //    {
        //        //if (Para.Cancel) return;
        //        //Thread.Sleep(200);
        //        backgroundWorker1.WorkerReportsProgress = true;
        //        backgroundWorker1.ReportProgress(i);
        //    }
        //}

        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    textBoxChooseType.Text = e.ProgressPercentage.ToString();
        //    //backgroundWorker1.Tex
        //    //this.Text = e.ProgressPercentage.ToString();
        //    this.Refresh();
        //}
    }
}
