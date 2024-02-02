# ANDROID
    DEBUG APK
    dotnet build -t:PackageForAndroid -p:Configuration=Debug

    APK ASSINADA
    export MallocStackLogging=no
     dotnet build -t:Build -p:Configuration=Release -p:AndroidPackageFormat=aab -maxCpuCount -p:UseSharedCompilation=true

    senha vs code
    andre@englobasistemas.com.br
    mfso-dgfj-xill-dwna
