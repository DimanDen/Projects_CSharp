using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ProgramForCreatingListing.Components
{
    public class TextBoxWithHint : TextBox, IComponentExecute
    {
        [Localizable(true)]
        public string Hint
        {
            get { return hint; }
            set { hint = value; updateHint(); }
        }

        private void updateHint()
        {
            if (this.IsHandleCreated && hint != null)
            {
                SendMessage(this.Handle, 0x1501, (IntPtr)1, hint);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            updateHint();
        }
        private string hint;

        public IMediator mediator;
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void Execute()
        {
            //mediator.ChangePerfomanceTextBox();
            //mediator.Notify("I am textbox");
        }
        public string GetName()
        {
            return Name;
        }
        

        // PInvoke
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);
    }
}
