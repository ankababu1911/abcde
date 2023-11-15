using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Mobile.Services.Navigation
{
    public class NavigationServices: INavigation
    {
       
        public IReadOnlyList<Page> ModalStack => Shell.Current?.Navigation?.ModalStack;
        public IReadOnlyList<Page> NavigationStack => Shell.Current?.Navigation?.NavigationStack;

        public void InsertPageBefore(Page page, Page before)
        {
            Shell.Current.Navigation.InsertPageBefore(page, before);
        }

        public Task<Page> PopAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            return Shell.Current.Navigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync()
        {
            return Shell.Current.Navigation.PopModalAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return Shell.Current.Navigation.PopModalAsync(animated);
        }

        public Task PopToRootAsync()
        {
            return Shell.Current.Navigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            return Shell.Current.Navigation.PopToRootAsync(animated);
        }

        public Task PushAsync(Page page)
        {
            return Shell.Current.Navigation.PushAsync(page);
        }

        public Task PushAsync(Page page, bool animated)
        {
            return Shell.Current.Navigation.PushAsync(page, animated);
        }

        public Task PushModalAsync(Page page)
        {
            return Shell.Current.Navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return Shell.Current.Navigation.PushModalAsync(page, animated);
        }

        public void RemovePage(Page page)
        {
            Shell.Current.Navigation.RemovePage(page);
        }
    }
}
