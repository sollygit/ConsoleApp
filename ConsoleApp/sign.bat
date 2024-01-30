::Signing files
signtool sign /a /v "%1\ConsoleApp.exe"
signtool sign /a /v "%1\ConsoleApp.dll"
signtool sign /a /v "%1\ConsoleApp.Common.dll"
signtool sign /a /v "%1\ConsoleApp.Models.dll"
signtool sign /a /v "%1\ConsoleApp.Services.dll"
signtool sign /a /v "%1\EasyConsoleCore.dll"
echo Signing files success.