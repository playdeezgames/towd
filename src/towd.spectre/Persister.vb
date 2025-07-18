Imports towd.ui

Friend Class Persister
    Implements IPersister

    Public Function SaveExists(saveSlot As ISaveSlot) As Boolean Implements IPersister.SaveExists
        Return saveSlot.SaveExists
    End Function
End Class
