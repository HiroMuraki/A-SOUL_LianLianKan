dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
$hasPath = Test-Path "OutputProgram/A-SOUL������.exe"
if ($hasPath) {
    Move-Item -force "OutputProgram/A-SOUL������.exe" "OutputProgram\Tokens\ASoul������.exe"
}
Rename-Item "OutputProgram\DianaLLK_GUI.exe" "A-SOUL������.exe"