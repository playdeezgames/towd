Public Interface IUIContext(Of TWorld)
    Inherits IUIDialog
    ReadOnly Property World As TWorld
    Sub SaveGame(saveSlot As String)
    Function LoadGame(saveSlot As String) As Boolean
    ReadOnly Property IsClosed As Boolean
End Interface
