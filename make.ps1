dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
ren "OutputProgram\DianaLLK_GUI.exe" "ASoul连连看.exe"