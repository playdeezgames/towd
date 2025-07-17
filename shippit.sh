rm -rf ./pub-linux
rm -rf ./pub-windows
rm -rf ./pub-mac
rm -rf ./pub-html
dotnet publish ./src/towd/towd.vbproj -o ./pub-linux -c Release --sc -p:PublishSingleFile=true -r linux-x64
dotnet publish ./src/towd/towd.vbproj -o ./pub-windows -c Release --sc -p:PublishSingleFile=true -r win-x64
dotnet publish ./src/towd/towd.vbproj -o ./pub-mac -c Release --sc -p:PublishSingleFile=true -r osx-x64
dotnet publish ./src/towd.spectre/towd.spectre.vbproj -o ./pub-linux -c Release --sc -p:PublishSingleFile=true -r linux-x64
dotnet publish ./src/towd.spectre/towd.spectre.vbproj -o ./pub-windows -c Release --sc -p:PublishSingleFile=true -r win-x64
dotnet publish ./src/towd.spectre/towd.spectre.vbproj -o ./pub-mac -c Release --sc -p:PublishSingleFile=true -r osx-x64
dotnet publish ./src/towd.blazor/towd.blazor/towd.blazor.csproj -o ./pub-html -c Release 
rm -f ./pub-linux/*.pdb
rm -f ./pub-windows/*.pdb
rm -f ./pub-mac/*.pdb
rm -f ./pub-html/*.pdb
butler push pub-windows thegrumpygamedev/towd:windows
butler push pub-linux thegrumpygamedev/towd:linux
butler push pub-mac thegrumpygamedev/towd:mac
#butler push pub-html/wwwroot thegrumpygamedev/towd:html
