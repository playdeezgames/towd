Imports towd.business

Friend Class MessageUIDialog
    Implements IUIDialog

    Friend Shared Function DetermineMessageDialog(context As IUIContext(Of IWorld), nextDialog As Func(Of IUIDialog)) As IUIDialog
        If context.World.Avatar?.HasMessages Then
            Return New MessageUIDialog(context, nextDialog)
        End If
        Return nextDialog()
    End Function

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly nextDialog As Func(Of IUIDialog)

    Public Sub New(context As IUIContext(Of IWorld), nextDialog As Func(Of IUIDialog))
        Me.context = context
        Me.nextDialog = nextDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(context.World.Avatar.CurrentMessage.Select(Function(x) (Mood.Normal, x, True)))
    End Function

    Public Function GetChoices() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoices
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return String.Empty
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        context.World.Avatar.DismissMessage()
        If context.World.Avatar.HasMessages Then
            Return Me
        End If
        Return nextDialog()
    End Function
End Class
