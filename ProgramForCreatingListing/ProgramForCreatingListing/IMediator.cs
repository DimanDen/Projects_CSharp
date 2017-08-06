using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForCreatingListing
{
    public interface IMediator
    {
        void Notify(string note);
        void RegisterComponent(ProgramForCreatingListing.Components.IComponentExecute component);
        void SearchFiles();
        void FormListing();
        void DeleteElementsFromList();
        void ChangePerfomanceTextBox();
        void ChooseAllElement();
        void ChooseTxtFormatListing();
        void ChooseWordFormatListing();
        void CheckStatus();
    }
}
