using Microsoft.Maui.Storage;

namespace App.Helpers;

public class SecureStorageHelper
{
    public static async Task<string> Get(string key)
    {
        try
        {
            var value = await SecureStorage.Default.GetAsync(key);

            return value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static async Task Set(string key, string value) =>
        await SecureStorage.Default.SetAsync(key, value);

    public static bool Remove(string key) => SecureStorage.Default.Remove(key);

    public static void Clear() => SecureStorage.Default.RemoveAll();
}
