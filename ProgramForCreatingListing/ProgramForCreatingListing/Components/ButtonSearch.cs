using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProgramForCreatingListing.Components
{
    public class ButtonSearch : Button, IComponentExecute
    {
        public IMediator mediator;
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void Execute() 
        {
            mediator.SearchFiles();
        }

        public string GetName()
        {
            return Name;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
        
    }
}
