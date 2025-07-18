using System.Text.Json;
using towd.data;
using towd.ui;

namespace towd.blazor.standalone
{
    public class Persister : IPersister
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

        public bool SaveExists(ISaveSlot saveSlot)
        {
            return File.Exists(saveSlot.Filename);
        }

        public void SaveGame(ISaveSlot saveSlot, WorldData worldData)
        {
            saveSlot.SaveGame(worldData);
        }
    }
}
