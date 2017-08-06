using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramForCreatingListing.Components
{
    class CheckBoxTxtType : CheckBox, IComponentExecute
    {
        public IMediator mediator;
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void Execute()
        {
            //mediator.ChooseTxtFormatListing();
        }

        public string GetName()
        {
            return Name;
        }
    }
}
