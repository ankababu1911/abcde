using abcde.Mobile.Services.AppEnvironment;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Mobile.ViewModels.Base
{
    public interface IViewModelBase : IQueryAttributable
    {
        public IAsyncRelayCommand InitializeAsyncCommand { get; }

        public IAppEnvironmentService appEnvironmentService { get; }

        public bool IsBusy { get; }

        public bool IsInitialized { get; }

        Task InitializeAsync();
    }
}
