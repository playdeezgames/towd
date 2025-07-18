Imports towd.data

Public Interface IPersister
    Sub SaveGame(saveSlot As ISaveSlot, worldData As WorldData)
    Function SaveExists(saveSlot As ISaveSlot) As Boolean
    Function LoadGame(saveSlot As ISaveSlot) As WorldData
End Interface
