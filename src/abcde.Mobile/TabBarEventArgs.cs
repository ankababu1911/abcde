using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static abcde.Mobile.AppShell;

namespace abcde.Mobile
{
    public class TabBarEventArgs: EventArgs
    {
        public PageType CurrentPage { get; private set; }

        public TabBarEventArgs(PageType currentPage)
        {
            CurrentPage = currentPage;
        }
    }
}
