Imports towd.data

Public Interface IPersister
    Sub SaveGame(SaveSlot As ISaveSlot, worldData As WorldData)
    Function SaveExists(saveSlot As ISaveSlot) As DateTime?
    Function LoadGame(saveSlot As ISaveSlot) As WorldData
End Interface
