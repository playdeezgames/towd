Imports towd.business

Friend Class SaveGameUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly table As New Dictionary(Of String, ISaveSlot)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Async Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
        table.Clear()
        For Each descriptor In SaveSlots.Descriptors.Values
            Dim saveExists = Await context.Persister.SaveExists(descriptor)
            Dim name = descriptor.DisplayName
            If saveExists.HasValue Then
                name &= $"(Last saved: {saveExists.Value})"
            End If
            table.Add(name, descriptor)
        Next
        result.AddRange(table.Keys)
        Return result
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Save Menu")
    End Function

    Public Async Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim saveSlot As ISaveSlot = Nothing
        If table.TryGetValue(choice, saveSlot) Then
            If (Await context.Persister.SaveExists(saveSlot)).HasValue Then
                Return New ConfirmUIDialog(
                    $"Are you sure you want to overwrite {saveSlot.DisplayName}",
                    Function() SaveGame(saveSlot),
                    Function() Me)
            End If
            Return SaveGame(saveSlot)
        End If
        Return cancelDialog()
    End Function

    Private Function SaveGame(saveSlot As ISaveSlot) As IUIDialog
        context.SaveGame(saveSlot.SaveSlot)
        Return New MessageBoxUIDialog("Game Saved!", {(Mood.Normal, $"Saved game in {saveSlot.DisplayName}", True)}, cancelDialog)
    End Function
End Class
