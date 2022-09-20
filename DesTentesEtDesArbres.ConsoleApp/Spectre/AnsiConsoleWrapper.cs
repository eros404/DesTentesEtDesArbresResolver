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
                        .Select(x => tilesStates[i, x].ToString()));
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
                        .Select(x => (i, x) == cursor ? "[red]+++[/]" : tilesStates[i, x].ToString()));
                table.AddRow(rowContent.ToArray());
            }
            AnsiConsole.Write(table);
        }
    }
}
