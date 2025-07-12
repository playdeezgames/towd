Imports towd.business

Public Interface IContext
    ReadOnly Property World As IWorld
    Sub SaveGame(saveSlot As String, notify As Action)
    Function LoadGame(saveSlot As String) As Boolean
End Interface
