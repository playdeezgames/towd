Friend Class DeedDetailUIDialog
    Implements IUIDialog

    Private ReadOnly deed As business.IDeed
    Private ReadOnly cancelDialog As Func(Of IUIDialog)

    Public Sub New(deed As business.IDeed, cancelDialog As Func(Of IUIDialog))
        Me.deed = deed
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))({(Mood.Normal, deed.Description, True)})
    End Function

    Public Function GetChoices() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoices
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return deed.Name
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return cancelDialog()
    End Function
End Class
