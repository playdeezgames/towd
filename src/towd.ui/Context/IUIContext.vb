Imports towd.business

Public Interface IUIContext(Of TWorld)
    ReadOnly Property World As TWorld
    Sub SaveGame(saveSlot As String)
    Function LoadGame(saveSlot As String) As Boolean
    Property Dialog As IUIDialog
End Interface
