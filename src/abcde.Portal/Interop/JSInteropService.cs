using Microsoft.JSInterop;

namespace abcde.Portal.Interop
{
    public interface IJSInteropService
    {
        Task OpenDocumentNewWindow(string url);
    }

    public class JSInteropService : IJSInteropService
    {
        private readonly IJSRuntime jsRuntime;

        public JSInteropService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task OpenDocumentNewWindow(string url)
        {
            await jsRuntime.InvokeVoidAsync("open", url, "_blank");
        }
    }
}
