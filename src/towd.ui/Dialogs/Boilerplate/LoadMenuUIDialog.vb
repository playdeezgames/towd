Imports towd.business

Friend Class LoadMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private ReadOnly table As New Dictionary(Of String, ISaveSlot)

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(Array.Empty(Of UIDialogLine))
    End Function

    Public Async Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        If Not table.Any Then
            table.Add(NEVER_MIND_TEXT, Nothing)
            For Each saveSlot In SaveSlots.Descriptors.Values
                Dim saveExists = Await context.Persister.SaveExists(saveSlot)
                If saveExists.HasValue Then
                    table.Add($"{saveSlot.DisplayName}(Last saved: {saveExists.Value})", saveSlot)
                End If
            Next
        End If
        Return table.Keys
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Load Menu")
    End Function

    Public Async Function Choose(choice As String, parameters As IReadOnlyDictionary(Of String, String)) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim saveSlot As ISaveSlot = table(choice)
        If saveSlot Is Nothing Then
            Return cancelDialog()
        End If
        If Await context.LoadGame(saveSlot.SaveSlot) Then
            Return New MessageBoxUIDialog("Load Success!", {New UIDialogLine(Mood.Normal, $"You loaded {saveSlot.DisplayName}!", True)}, Function() NeutralUIDialog.DetermineInPlayDialog(context))
        Else
            Return New MessageBoxUIDialog("Load Failed!", {New UIDialogLine(Mood.Normal, $"Failed to load {saveSlot.DisplayName}!", True)}, cancelDialog)
        End If
        Throw New NotImplementedException
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New LoadMenuUIDialog(context, cancelDialog))
    End Function
End Class
