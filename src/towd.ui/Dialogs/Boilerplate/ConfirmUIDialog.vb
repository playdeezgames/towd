Friend Class ConfirmUIDialog
    Implements IUIDialog

    Private ReadOnly caption As String
    Private ReadOnly confirmDialog As Func(Of IUIDialog)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)

    Const YES_TEXT = "Yes"
    Const NO_TEXT = "No"

    Public Sub New(caption As String, confirmDialog As Func(Of IUIDialog), cancelDialog As Func(Of IUIDialog))
        Me.caption = caption
        Me.confirmDialog = confirmDialog
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(Array.Empty(Of UIDialogLine))
    End Function

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({NO_TEXT, YES_TEXT})
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(caption)
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case YES_TEXT
                Return Task.FromResult(confirmDialog())
            Case NO_TEXT
                Return Task.FromResult(cancelDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New ConfirmUIDialog(caption, confirmDialog, cancelDialog))
    End Function
End Class
