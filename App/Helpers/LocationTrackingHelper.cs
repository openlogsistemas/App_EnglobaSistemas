namespace App.Services;

public class LocationTrackingService
{
    static CancellationTokenSource? _cts;

    public static async Task StartTrackingAsync()
    {
        _cts = new CancellationTokenSource();

        await Task.Run(
            async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    AuthService? authService = MauiProgram.Services.GetService<AuthService>();
                    if (authService is null)
                        continue;

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            var location = await Geolocation.GetLastKnownLocationAsync();
                            if (location != null)
                            {
                                authService.Latitude = location.Latitude;
                                authService.Longitude = location.Longitude;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    });

                    await Task.Delay(3000);
                }
            },
            _cts.Token
        );
    }

    public static void StopTracking()
    {
        _cts?.Cancel();
    }
}
