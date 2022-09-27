using DesTentesEtDesArbres.Data;
using DesTentesEtDesArbres.ConsoleApp.Spectre;
using DesTentesEtDesArbres.Core;
using Spectre.Console;

namespace DesTentesEtDesArbres.ConsoleApp.Workflows
{
    public static class TestLevelWorkflow
    {
        public static void Start()
        {
            AnsiConsole.Clear();
            var dbContext = new DesTentesEtDesArbresContext();
            var allLevels = dbContext.LevelDefinitions.ToList();
            if (!allLevels.Any())
            {
                AnsiConsole.MarkupLine("[red]No level[/]");
                return;
            }
            var selectedLevelName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Level to test:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more levels)[/]")
                    .AddChoices(allLevels.Select(l => l.Name)));
            var selectedLevel = allLevels.Find(l => l.Name == selectedLevelName);
            if (selectedLevel == null)
                return;
            var playgroundInitializer = selectedLevel.PlaygroundInitializer;
            if (playgroundInitializer == null || playgroundInitializer.TilesStates == null || playgroundInitializer.NumberOfTreesByRow == null || playgroundInitializer.NumberOfTreesByColumn == null)
                return;
            var playground = new Playground(playgroundInitializer.TilesStates, playgroundInitializer.NumberOfTreesByRow, playgroundInitializer.NumberOfTreesByColumn);
            var resolver = new Resolver(playground);
            int step = 1;
            if (AnsiConsole.Confirm("Debug mode ?", false))
                resolver.StateChanged += ResolverStateChangedHandler;
            AnsiConsole.Clear();
            AnsiConsoleWrapper.DisplayPlayground(selectedLevelName, playground.GetTileStateMatrix(),
                playground.NumberOfTreesByRow, playground.NumberOfTreesByColumn);
            resolver.Resolve();
            AnsiConsoleWrapper.DisplayPlayground($"{selectedLevelName} [green]Result[/]", playground.GetTileStateMatrix(),
                playground.NumberOfTreesByRow, playground.NumberOfTreesByColumn);

            void ResolverStateChangedHandler(object? sender, EventArgs args)
            {
                AnsiConsoleWrapper.DisplayPlayground($"{selectedLevelName} [blue]Step {step}[/]", playground.GetTileStateMatrix(),
                    playground.NumberOfTreesByRow, playground.NumberOfTreesByColumn);
                step++;
            }
        }
    }
}
