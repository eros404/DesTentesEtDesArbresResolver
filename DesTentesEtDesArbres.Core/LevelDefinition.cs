using Newtonsoft.Json;

namespace DesTentesEtDesArbres.Core
{
    public class LevelDefinition
    {
        public uint Height { get; set; }
        public uint Width { get; set; }
        public char Letter { get; set; }
        public uint Number { get; set; }
        public string Name => $"{Height}x{Width} {Letter}{Number}";
        public LevelDifficulty Difficulty { get; set; }
        public string SerializedPlaygroundInitializer { get; set; } = string.Empty;
        public PlaygroundInitializer? PlaygroundInitializer =>
            JsonConvert.DeserializeObject<PlaygroundInitializer>(SerializedPlaygroundInitializer);
        public void SetSerializedPlaygroundInitializer(PlaygroundInitializer playgroundInitializer)
        {
            SerializedPlaygroundInitializer = JsonConvert.SerializeObject(playgroundInitializer);
        }
    }
}
