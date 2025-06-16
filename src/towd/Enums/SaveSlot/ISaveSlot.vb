Imports towd.business

Public Interface ISaveSlot
    ReadOnly Property SaveSlot As SaveSlot
    ReadOnly Property DisplayName As String
    ReadOnly Property Filename As String
    Sub SaveGame(world As IWorld)
End Interface
