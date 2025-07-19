Imports towd.business

Friend Class DialogUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly dialog As IDialog

    Public Sub New(context As IUIContext(Of IWorld), dialog As IDialog)
        Me.context = context
        Me.dialog = dialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(dialog.Lines.Select(Function(x) (Mood.Normal, x, True)))
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
            Return Task.FromResult(MessageUIDialog.DetermineMessageDialog(context, Function() New DialogUIDialog(context, nextDialog)))
        End If
        Return Task.FromResult(NeutralUIDialog.DetermineInPlayDialog(context))
    End Function
End Class
