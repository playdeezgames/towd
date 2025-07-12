Imports towd.business

Public Interface IUIContext
    ReadOnly Property World As IWorld
    Sub SaveGame(saveSlot As String, notify As Action)
    Function LoadGame(saveSlot As String) As Boolean
    Property Dialog As IUIDialog
    Property GameState As String
End Interface
