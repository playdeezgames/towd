using Microsoft.JSInterop;
using System.Text.Json;
using towd.data;
using towd.ui;

namespace towd.blazor.standalone
{
    public class Persister(IJSRuntime JSRuntime) : IPersister
    {
        public async Task<WorldData> LoadGame(ISaveSlot saveSlot)
        {
            var data = await JSRuntime.InvokeAsync<string>("loadGame", saveSlot.Filename);
            if(data != null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return JsonSerializer.Deserialize<WorldData>(data);
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<DateTime?> SaveExists(ISaveSlot saveSlot)
        {
            var result = await JSRuntime.InvokeAsync<string>("saveExists", saveSlot.Filename);
            if(result==null)
            {
                return null;
            }
            return DateTime.Parse(result);
        }

        public async Task SaveGame(ISaveSlot saveSlot, WorldData worldData)
        {
            await JSRuntime.InvokeVoidAsync("saveGame", saveSlot.Filename, JsonSerializer.Serialize(worldData));
        }
    }
}
