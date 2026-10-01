using Microsoft.JSInterop;

namespace YumBlazor.Services.Extensions
{
    public static class IJSRuntimeExtensions
    {

        public static async Task ToastrSuccess(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showToastr", "success", message);
        }

        public static async Task ToastrError(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showToastr", "error", message);
        }

        public static async Task ToastrWarning(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showToastr", "warning", message);
        }

        public static async Task SweetAlertSuccess(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showSweetAlert", "success", message);
        }

        public static async Task SweetAlertError(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showSweetAlert", "error", message);
        }

        public static async Task SweetAlertWarning(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showSweetAlert", "warning", message);
        }



    }
}
