dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
$hasPath = Test-Path "OutputProgram/ASoul连连看.exe"
if ($hasPath) {
    move -force "OutputProgram/ASoul连连看.exe" "OutputProgram\Tokens\ASoul连连看.exe"
}
ren "OutputProgram\DianaLLK_GUI.exe" "ASoul连连看.exe"