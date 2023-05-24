using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerMAUI.Views
{
    public interface IDisplayMessageAndGoBack
    {
        void ShowMessage(string message);
        void GoBack();
    }
}
