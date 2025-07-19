Imports towd.business

Friend Class LoadMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private table As New Dictionary(Of String, ISaveSlot)

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table.Add(NEVER_MIND_TEXT, Nothing)
        For Each saveSlot In SaveSlots.Descriptors.Values
            Dim saveExists = context.Persister.SaveExists(saveSlot)
            If saveExists.HasValue Then
                table.Add($"{saveSlot.DisplayName}(Last saved: {saveExists.Value})", saveSlot)
            End If
        Next
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))(table.Keys)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Load Menu")
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim saveSlot As ISaveSlot = table(choice)
        If saveSlot Is Nothing Then
            Return Task.FromResult(cancelDialog())
        End If
        If context.LoadGame(saveSlot.SaveSlot) Then
            Return Task.FromResult(Of IUIDialog)(New MessageBoxUIDialog("Load Success!", {(Mood.Normal, $"You loaded {saveSlot.DisplayName}!", True)}, Function() NeutralUIDialog.DetermineInPlayDialog(context)))
        Else
            Return Task.FromResult(Of IUIDialog)(New MessageBoxUIDialog("Load Failed!", {(Mood.Normal, $"Failed to load {saveSlot.DisplayName}!", True)}, cancelDialog))
        End If
        Throw New NotImplementedException
    End Function
End Class
