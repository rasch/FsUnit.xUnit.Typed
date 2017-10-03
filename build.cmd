dotnet test -l Appveyor
dotnet build -c Release
.paket\paket.exe pack .build/nugets