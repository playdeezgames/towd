Public Class MessageBoxUIDialog
    Implements IUIDialog
    Sub New(prompt As String, lines As UIDialogLine(), nextDialog As Func(Of IUIDialog))
        Me._Prompt = prompt
        Me._Lines = lines
        Me.nextDialog = nextDialog
    End Sub

    Private _Lines As IEnumerable(Of UIDialogLine)

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(_Lines)
    End Function

    Private ReadOnly nextDialog As Func(Of IUIDialog)

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Private _Prompt As String

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(_Prompt)
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Return Task.FromResult(nextDialog())
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New MessageBoxUIDialog(_Prompt, _Lines.ToArray, nextDialog))
    End Function
End Class
