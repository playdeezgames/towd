Public Interface IUIContext(Of TWorld)
    Inherits IUIDialog
    ReadOnly Property World As TWorld
    Function SaveGame(saveSlot As String) As Task
    Function LoadGame(saveSlot As String) As Task(Of Boolean)
    ReadOnly Property IsClosed As Boolean
    ReadOnly Property Persister As IPersister
End Interface
