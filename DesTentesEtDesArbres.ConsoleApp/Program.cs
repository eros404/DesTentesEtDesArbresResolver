using DesTentesEtDesArbres.ConsoleApp.Data;
using DesTentesEtDesArbres.ConsoleApp.Spectre;
using DesTentesEtDesArbres.ConsoleApp.Workflows;
using DesTentesEtDesArbres.Core;
using Spectre.Console;

var dbContext = new DesTentesEtDesArbresContext();

var quit = false;
do
{
    AnsiConsoleWrapper.Selection(new Dictionary<string, Action>()
    {
        { "New level", () => dbContext.Add(NewLevelWorkflow.Start()) },
        { "Quit", () => quit = true }
    });
} while (!quit);