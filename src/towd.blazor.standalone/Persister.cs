using Microsoft.JSInterop;
using System.Text.Json;
using towd.data;
using towd.ui;

namespace towd.blazor.standalone
{
    public class Persister(IJSRuntime JSRuntime) : IPersister
    {
        public WorldData LoadGame(ISaveSlot saveSlot)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return JsonSerializer.Deserialize<WorldData>(File.ReadAllText(saveSlot.Filename));
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public DateTime? SaveExists(ISaveSlot saveSlot)
        {
            return File.Exists(saveSlot.Filename) ? File.GetLastWriteTime(saveSlot.Filename) : null;
        }

        public void SaveGame(ISaveSlot saveSlot, WorldData worldData)
        {
            File.WriteAllText(saveSlot.Filename, JsonSerializer.Serialize(worldData));
        }
    }
}
