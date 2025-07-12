Imports towd.business
Imports towd.data

Public Class Context
    Implements IContext
    Private worldData As New WorldData
    Public ReadOnly Property World As IWorld Implements IContext.World
        Get
            Return New World(worldData)
        End Get
    End Property
    Public Sub SaveGame(saveSlot As String, notify As Action) Implements IContext.SaveGame
        saveSlot.ToSaveSlotDescriptor.SaveGame(worldData)
        If notify IsNot Nothing Then
            notify()
        End If
    End Sub
    Public Function LoadGame(saveSlot As String) As Boolean Implements IContext.LoadGame
        Dim loadAttempt = saveSlot.ToSaveSlotDescriptor.LoadGame()
        If loadAttempt IsNot Nothing Then
            worldData = loadAttempt
            Return True
        Else
            Return False
        End If
    End Function
End Class
