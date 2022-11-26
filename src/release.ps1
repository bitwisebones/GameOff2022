dotnet build -c Release --sc --runtime win-x64
dotnet build -c Release --sc --runtime linux-x64
dotnet build -c Release --sc --runtime osx-x64

Remove-Item 'bin/Release/net6.0/win-x64/Resources/Source' -Recurse
Remove-Item 'bin/Release/net6.0/linux-x64/Resources/Source' -Recurse
Remove-Item 'bin/Release/net6.0/osx-x64/Resources/Source' -Recurse

Compress-Archive -Path 'bin/Release/net6.0/win-x64' -DestinationPath 'MouseOrMan-windows.zip'
Compress-Archive -Path 'bin/Release/net6.0/linux-x64' -DestinationPath 'MouseOrMan-linux.zip'
Compress-Archive -Path 'bin/Release/net6.0/osx-x64' -DestinationPath 'MouseOrMan-osx.zip'