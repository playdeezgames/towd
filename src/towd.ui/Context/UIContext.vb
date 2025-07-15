Imports towd.business
Imports towd.data

Public Class UIContext
    Implements IUIContext
    Private worldData As New WorldData
    Public ReadOnly Property World As IWorld Implements IUIContext.World
        Get
            Return New World(worldData)
        End Get
    End Property

    Public Property Dialog As IUIDialog Implements IUIContext.Dialog
    Public Sub SaveGame(saveSlot As String) Implements IUIContext.SaveGame
        saveSlot.ToSaveSlotDescriptor.SaveGame(worldData)
    End Sub
    Public Function LoadGame(saveSlot As String) As Boolean Implements IUIContext.LoadGame
        Dim loadAttempt = saveSlot.ToSaveSlotDescriptor.LoadGame()
        If loadAttempt IsNot Nothing Then
            worldData = loadAttempt
            Return True
        Else
            Return False
        End If
    End Function
End Class
