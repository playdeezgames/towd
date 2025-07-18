Imports towd.data

Public Interface IPersister
    Function SaveGameAsync(SaveSlot As ISaveSlot, worldData As WorldData) As Task
    Function SaveExists(saveSlot As ISaveSlot) As DateTime?
    Function LoadGame(saveSlot As ISaveSlot) As WorldData
End Interface
