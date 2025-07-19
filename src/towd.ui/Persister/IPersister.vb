Imports towd.data

Public Interface IPersister
    Function SaveGame(SaveSlot As ISaveSlot, worldData As WorldData) As Task
    Function SaveExists(saveSlot As ISaveSlot) As Task(Of DateTime?)
    Function LoadGame(saveSlot As ISaveSlot) As Task(Of WorldData)
End Interface
