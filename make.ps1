dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
$hasPath = Test-Path "OutputProgram/ASoul������.exe"
if ($hasPath) {
    move -force "OutputProgram/ASoul������.exe" "OutputProgram\Tokens\ASoul������.exe"
}
ren "OutputProgram\DianaLLK_GUI.exe" "ASoul������.exe"