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

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return Array.Empty(Of (String, String, Boolean))
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Return table.Keys
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return "Load Menu"
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim saveSlot As ISaveSlot = table(choice)
        If saveSlot Is Nothing Then
            Return cancelDialog()
        End If
        If context.LoadGame(saveSlot.SaveSlot) Then
            Return New MessageBoxUIDialog("Load Success!", {(Mood.Normal, $"You loaded {saveSlot.DisplayName}!", True)}, Function() NeutralUIDialog.DetermineInPlayDialog(context))
        Else
            Return New MessageBoxUIDialog("Load Failed!", {(Mood.Normal, $"Failed to load {saveSlot.DisplayName}!", True)}, cancelDialog)
        End If
        Throw New NotImplementedException
    End Function
End Class
