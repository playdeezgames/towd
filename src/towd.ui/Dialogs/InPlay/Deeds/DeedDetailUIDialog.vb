Friend Class DeedDetailUIDialog
    Implements IUIDialog

    Private ReadOnly deed As business.IDeed
    Private ReadOnly cancelDialog As Func(Of IUIDialog)

    Public Sub New(deed As business.IDeed, cancelDialog As Func(Of IUIDialog))
        Me.deed = deed
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))({New UIDialogLine(Mood.Normal, deed.Description, True)})
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(deed.Name)
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Return Task.FromResult(cancelDialog())
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New DeedDetailUIDialog(deed, cancelDialog))
    End Function
End Class
