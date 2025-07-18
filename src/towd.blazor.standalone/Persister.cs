using towd.ui;

namespace towd.blazor.standalone
{
    public class Persister : IPersister
    {
        public bool SaveExists(ISaveSlot saveSlot)
        {
            return saveSlot.SaveExists;
        }
    }
}
