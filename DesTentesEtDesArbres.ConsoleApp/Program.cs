using DesTentesEtDesArbres.ConsoleApp.Spectre;
using DesTentesEtDesArbres.ConsoleApp.Workflows;

var quit = false;
do
{
    AnsiConsoleWrapper.Selection(new Dictionary<string, Action>()
    {
        { "Test Level", TestLevelWorkflow.Start },
        { "Add level", NewLevelWorkflow.Start },
        { "Quit", () => quit = true }
    });
} while (!quit);