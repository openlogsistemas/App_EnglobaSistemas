# ANDROID
    dotnet build -c Release
    dotnet build -c Release -f net8.0-android
    dotnet publish -c Release -f:net8.0-android /p:AndroidPackageFormat=aab
