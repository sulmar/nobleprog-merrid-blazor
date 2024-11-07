using Microsoft.JSInterop;

namespace BlazorWebAssemblyApp.Authorization;

public class LocalStorage
{
    private readonly IJSRuntime js;

    public LocalStorage(IJSRuntime js)
    {
        this.js = js;
    }

    public async Task SetItem(string key, string value)
    {
        await js.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    public async Task<string> GetItem(string key)
    {
        return await js.InvokeAsync<string>("localStorage.getItem", key);
    }

    public async Task RemoveItem(string key)
    {
        await js.InvokeVoidAsync("localStorage.removeItem", key);
    }

}
