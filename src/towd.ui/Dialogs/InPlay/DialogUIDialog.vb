Imports towd.business

Friend Class DialogUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly dialog As IDialog

    Public Sub New(context As IUIContext(Of IWorld), dialog As IDialog)
        Me.context = context
        Me.dialog = dialog
    End Sub

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(dialog.Lines.Select(Function(x) New UIDialogLine(Mood.Normal, x, True)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(dialog.Choices)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(dialog.Prompt)
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim nextDialog = dialog.Choose(choice)
        If nextDialog IsNot Nothing Then
            Return Task.FromResult(MessageUIDialog.DetermineMessageDialog(context, MakeCopy))
        End If
        Return Task.FromResult(NeutralUIDialog.DetermineInPlayDialog(context))
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New DialogUIDialog(context, dialog))
    End Function
End Class
