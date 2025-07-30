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

    Public Async Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Await Dialog.GetLinesAsync()
    End Function

    Public Async Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Await Dialog.GetChoicesAsync()
    End Function

    Public Async Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Await Dialog.GetPromptAsync()
    End Function

    Public ReadOnly Property IsClosed As Boolean Implements IUIContext(Of IWorld).IsClosed
        Get
            Return Dialog Is Nothing
        End Get
    End Property

    Public ReadOnly Property Persister As IPersister Implements IUIContext(Of IWorld).Persister

    Public Async Function SaveGame(saveSlot As String) As Task Implements IUIContext(Of IWorld).SaveGame
        Await Persister.SaveGame(saveSlot.ToSaveSlotDescriptor, worldData)
    End Function
    Public Async Function LoadGame(saveSlot As String) As Task(Of Boolean) Implements IUIContext(Of IWorld).LoadGame
        Dim loadAttempt As WorldData = Await Persister.LoadGame(saveSlot.ToSaveSlotDescriptor)
        If loadAttempt IsNot Nothing Then
            worldData = loadAttempt
            Return True
        Else
            Return False
        End If
    End Function

    Public Async Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dialog = Await Dialog.Choose(choice)
        Return Me
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() Me)
    End Function

    Public Async Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Await Dialog.GetParametersAsync()
    End Function
End Class
