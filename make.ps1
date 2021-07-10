dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
$hasPath = Test-Path "OutputProgram/A-SOUL连连看.exe"
if ($hasPath) {
    Move-Item -force "OutputProgram/A-SOUL连连看.exe" "OutputProgram\Tokens\ASoul连连看.exe"
}
Rename-Item "OutputProgram\DianaLLK_GUI.exe" "A-SOUL连连看.exe"