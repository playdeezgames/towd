Imports towd.business
Imports towd.data

Public Interface ISaveSlot
    ReadOnly Property SaveSlot As SaveSlot
    ReadOnly Property DisplayName As String
    ReadOnly Property Filename As String
    ReadOnly Property SaveExists As Boolean
    Sub SaveGame(worldData As WorldData)
    Function LoadGame() As WorldData
End Interface
