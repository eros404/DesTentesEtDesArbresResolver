using DesTentesEtDesArbres.Core;
using Spectre.Console;

namespace DesTentesEtDesArbres.ConsoleApp.Spectre
{
    public static class AnsiConsoleWrapper
    {
        public static async Task<string> SelectionAsync(Dictionary<string, Func<Task>> actions)
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Choose an action[/]")
                    .AddChoices(actions.Keys)
                );
            await actions[selection]();
            return selection;
        }
        public static string Selection(Dictionary<string, Action> actions)
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Choose an action[/]")
                    .AddChoices(actions.Keys)
                );
            actions[selection]();
            return selection;
        }
        public static void DisplayPlayground(string name, TileState[,] tilesStates, uint[] numberOfTreesByRow, uint[] numberOfTreesByColumn)
        {
            var table = new Table
            {
                Title = new TableTitle(name)
            };
            table.AddColumn("");
            foreach (var numberOfTree in numberOfTreesByColumn)
                table.AddColumn(numberOfTree.ToString());
            table.Columns[0].Padding(2, 2);
            for (int i = 0; i < numberOfTreesByRow.Length; i++)
            {
                var rowContent = new List<string>() { numberOfTreesByRow[i].ToString() };
                rowContent.AddRange(Enumerable.Range(0, numberOfTreesByColumn.Length)
                        .Select(x => GetTileStateMarkup(tilesStates[i, x])));
                table.AddRow(rowContent.ToArray());
            }
            AnsiConsole.Write(table);
        }
        public static void DisplayPlaygroundForTreeSelection((uint x, uint y) cursor, TileState[,] tilesStates, uint[] numberOfTreesByRow, uint[] numberOfTreesByColumn)
        {
            var table = new Table
            {
                Title = new TableTitle("Plant the trees", new Style(Color.Green))
            };
            table.AddColumn("");
            foreach (var numberOfTree in numberOfTreesByColumn)
                table.AddColumn(numberOfTree.ToString());
            table.Columns[0].Padding(2, 2);
            for (int i = 0; i < numberOfTreesByRow.Length; i++)
            {
                var rowContent = new List<string>() { numberOfTreesByRow[i].ToString() };
                rowContent.AddRange(Enumerable.Range(0, numberOfTreesByColumn.Length)
                        .Select(x => (i, x) == cursor ? GetCursorMarkup(tilesStates[i, x]) : GetTileStateMarkup(tilesStates[i, x])));
                table.AddRow(rowContent.ToArray());
            }
            AnsiConsole.Write(table);
        }
        private static string GetTileStateMarkup(TileState tilestate)
        {
            return tilestate switch
            {
                TileState.Tree => "[darkgreen]Tree[/]",
                TileState.Grass => "[green3]Grass[/]",
                TileState.Tent => "[maroon]Tent[/]",
                TileState.Unknown => "[grey]Unknown[/]",
                _ => tilestate.ToString(),
            };
        }
        private static string GetCursorMarkup(TileState tilestate)
        {
            return tilestate switch
            {
                TileState.Tree => "[red]---[/]",
                _ => "[darkgreen]+++[/]",
            };
        }
    }
}
