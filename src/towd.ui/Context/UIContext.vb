Imports towd.business
Imports towd.data

Public Class UIContext
    Implements IUIContext(Of IWorld)
    Private worldData As New WorldData
    Public ReadOnly Property World As IWorld Implements IUIContext(Of IWorld).World
        Get
            Return New World(worldData)
        End Get
    End Property

    Public Property Dialog As IUIDialog Implements IUIContext(Of IWorld).Dialog

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return Dialog.Lines
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return Dialog.Choices
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return Dialog.Prompt
        End Get
    End Property

    Public Sub SaveGame(saveSlot As String) Implements IUIContext(Of IWorld).SaveGame
        saveSlot.ToSaveSlotDescriptor.SaveGame(worldData)
    End Sub
    Public Function LoadGame(saveSlot As String) As Boolean Implements IUIContext(Of IWorld).LoadGame
        Dim loadAttempt = saveSlot.ToSaveSlotDescriptor.LoadGame()
        If loadAttempt IsNot Nothing Then
            worldData = loadAttempt
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dialog = Dialog.Choose(choice)
        Return Me
    End Function
End Class
