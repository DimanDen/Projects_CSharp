using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForCreatingListing.Components
{
    public interface IComponentExecute
    {
        string GetName();
        void SetMediator(IMediator mediator);
        void Execute();
    }
}
