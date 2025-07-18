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

    Private Property Dialog As IUIDialog

    Sub New(persister As IPersister)
        Dialog = New SplashUIDialog(Me)
        Me.Persister = persister
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return Dialog.GetLines()
    End Function

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

    Public ReadOnly Property IsClosed As Boolean Implements IUIContext(Of IWorld).IsClosed
        Get
            Return Dialog Is Nothing
        End Get
    End Property

    Public ReadOnly Property Persister As IPersister Implements IUIContext(Of IWorld).Persister

    Public Sub SaveGame(saveSlot As String) Implements IUIContext(Of IWorld).SaveGame
        Persister.SaveGame(saveSlot.ToSaveSlotDescriptor, worldData)
    End Sub
    Public Function LoadGame(saveSlot As String) As Boolean Implements IUIContext(Of IWorld).LoadGame
        Dim loadAttempt As WorldData = Persister.LoadGame(saveSlot.ToSaveSlotDescriptor)
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
