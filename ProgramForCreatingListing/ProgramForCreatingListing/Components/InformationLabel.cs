using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramForCreatingListing.Components
{
    class InformationLabel : Label, IComponentExecute
    {
        public IMediator mediator;
        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void Execute()
        {
        }

        public string GetName()
        {
            return Name;
        }
    }
}
