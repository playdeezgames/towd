Imports towd.business

Friend Class DeadUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoices() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoices
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return "Yer dead."
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        context.World.Abandon()
        Return New MainMenuUIDialog(context)
    End Function
End Class
