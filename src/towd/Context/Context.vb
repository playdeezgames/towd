Imports towd.business
Imports towd.data

Public Class Context
    Implements IContext
    Private ReadOnly worldData As New WorldData
    Public ReadOnly Property World As IWorld Implements IContext.World
        Get
            Return New World(worldData)
        End Get
    End Property
    Public Sub SaveGame(saveSlot As String, notify As Action(Of String))
        saveSlot.ToSaveSlotDescriptor.SaveGame(worldData)
        If notify IsNot Nothing Then
            notify(saveSlot.ToSaveSlotDescriptor.DisplayName)
        End If
    End Sub
End Class
