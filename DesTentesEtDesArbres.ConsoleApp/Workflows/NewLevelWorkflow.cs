using DesTentesEtDesArbres.ConsoleApp.Data;
using DesTentesEtDesArbres.ConsoleApp.Spectre;
using DesTentesEtDesArbres.Core;
using Spectre.Console;

namespace DesTentesEtDesArbres.ConsoleApp.Workflows
{
    public static class NewLevelWorkflow
    {
        public static void Start()
        {
            AnsiConsole.Clear();
            var levelDefinition = new LevelDefinition
            {
                Height = AnsiConsole.Prompt(
                    new TextPrompt<uint>("Height:")),
                Width = AnsiConsole.Prompt(
                    new TextPrompt<uint>("Width:")),
                Letter = AnsiConsole.Prompt(
                    new TextPrompt<char>("Letter:")),
                Number = AnsiConsole.Prompt(
                    new TextPrompt<uint>("Number:")),
                Difficulty = AnsiConsole.Prompt(
                    new SelectionPrompt<LevelDifficulty>()
                        .Title("Difficulty:")
                        .AddChoices(new[] { LevelDifficulty.Easy, LevelDifficulty.Hard, LevelDifficulty.UnknownValues }))
            };
            var playgroundInitializer = new PlaygroundInitializer
            {
                TilesStates = new TileState[levelDefinition.Height, levelDefinition.Width],
                NumberOfTreesByRow = AnsiConsole.Prompt(
                    new TextPrompt<string>("Number of trees by row (separate by a coma):")
                        .Validate(content =>
                        {
                            var splittedContent = content.Split(',').ToList();
                            return splittedContent
                                .Select(nb => UInt32.TryParse(nb, out _))
                                .Any(parseResult => parseResult == true) &&
                                splittedContent.Count == levelDefinition.Height;
                        })
                    ).Split(',')
                    .ToList()
                    .Select(nb => UInt32.Parse(nb))
                    .ToArray(),
                NumberOfTreesByColumn = AnsiConsole.Prompt(
                    new TextPrompt<string>("Number of trees by column (separate by a coma):")
                        .Validate(content =>
                        {
                            var splittedContent = content.Split(',').ToList();
                            return splittedContent
                                .Select(nb => UInt32.TryParse(nb, out _))
                                .Any(parseResult => parseResult == true) &&
                                splittedContent.Count == levelDefinition.Width;
                        })
                    ).Split(',')
                    .ToList()
                    .Select(nb => UInt32.Parse(nb))
                    .ToArray()
            };
            for (uint i = 0; i < levelDefinition.Height; i++)
                for (uint y = 0; y < levelDefinition.Width; y++)
                    playgroundInitializer.TilesStates[i, y] = TileState.Unknown;
            ConsoleKey keyPressed;
            (uint x, uint y) coordonate = (0, 0);
            do
            {
                AnsiConsole.Clear();
                AnsiConsoleWrapper.DisplayPlaygroundForTreeSelection(coordonate, playgroundInitializer.TilesStates,
                    playgroundInitializer.NumberOfTreesByRow, playgroundInitializer.NumberOfTreesByColumn);
                AnsiConsole.MarkupLine("[green]Arrows[/] to move");
                AnsiConsole.MarkupLine("[green]Spacebar[/] to place or remove a tree");
                AnsiConsole.MarkupLine("[green]Enter[/] to confirm");
                keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.Spacebar)
                    playgroundInitializer.TilesStates[coordonate.x, coordonate.y] =
                        playgroundInitializer.TilesStates[coordonate.x, coordonate.y] == TileState.Tree ? TileState.Unknown : TileState.Tree;
                else if (keyPressed == ConsoleKey.UpArrow && coordonate.x > 0)
                    coordonate.x--;
                else if (keyPressed == ConsoleKey.DownArrow && coordonate.x < levelDefinition.Height - 1)
                    coordonate.x++;
                else if (keyPressed == ConsoleKey.LeftArrow && coordonate.y > 0)
                    coordonate.y--;
                else if (keyPressed == ConsoleKey.RightArrow && coordonate.y < levelDefinition.Width - 1)
                    coordonate.y++;
            } while (keyPressed != ConsoleKey.Enter);
            levelDefinition.SetSerializedPlaygroundInitializer(playgroundInitializer);
            var dbContext = new DesTentesEtDesArbresContext();
            dbContext.Add(levelDefinition);
            dbContext.SaveChanges();
        }
    }
}
