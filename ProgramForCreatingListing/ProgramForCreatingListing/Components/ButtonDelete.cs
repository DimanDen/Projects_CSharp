using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramForCreatingListing.Components
{
    class ButtonDelete : Button, IComponentExecute
    {
        public IMediator mediator;
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void Execute()
        {
            mediator.DeleteElementsFromList();
        }

        public string GetName()
        {
            return Name;
        }
    }
}
