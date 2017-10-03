dotnet test -l trx
dotnet build -c Release
.paket\paket.exe pack .build/nugets