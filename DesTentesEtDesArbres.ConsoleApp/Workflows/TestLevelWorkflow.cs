using DesTentesEtDesArbres.ConsoleApp.Data;
using DesTentesEtDesArbres.ConsoleApp.Spectre;
using DesTentesEtDesArbres.Core;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesTentesEtDesArbres.ConsoleApp.Workflows
{
    public static class TestLevelWorkflow
    {
        public static void Start()
        {
            var dbContext = new DesTentesEtDesArbresContext();
            var allLevels = dbContext.LevelDefinitions.ToList();
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
            AnsiConsole.Clear();
            AnsiConsoleWrapper.DisplayPlayground(selectedLevelName, playground.GetTileStateMatrix(),
                playground.NumberOfTreesByRow, playground.NumberOfTreesByColumn);
            resolver.Resolve();
            AnsiConsoleWrapper.DisplayPlayground($"{selectedLevelName} [green]Solved[/]", playground.GetTileStateMatrix(),
                playground.NumberOfTreesByRow, playground.NumberOfTreesByColumn);
        }
    }
}
