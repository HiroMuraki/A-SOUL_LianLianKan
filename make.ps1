dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
$hasPath = Test-Path "OutputProgram/ASoul������.exe"
if ($hasPath) {
    Move-Item -force "OutputProgram/ASoul������.exe" "OutputProgram\Tokens\ASoul������.exe"
}
Rename-Item "OutputProgram\DianaLLK_GUI.exe" "ASoul������.exe"