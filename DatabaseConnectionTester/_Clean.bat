:: Clean a Visual Studio project by removing the bin and obj directories.
:: This script was created because Visual Studio's Clean command doesn't
:: always remove the artifacts from the build.
:: 2/27/2017 Alex Mammay

ECHO "Clean the Visual Studio Solution by removing the bin and obj directories"

:: Look for "bin", "obj", "Setup", and "BuildResults" directories and remove them
for /d /r /.vs . %%d in (bin,obj,Setup,BuildResults) do @if exist "%%d" rd /s/q "%%d"

del *.log /Q
del *.suo 