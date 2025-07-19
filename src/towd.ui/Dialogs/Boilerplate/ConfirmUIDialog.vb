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

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Return {NO_TEXT, YES_TEXT}
    End Function

    Private Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return caption
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Select Case choice
            Case YES_TEXT
                Return confirmDialog()
            Case NO_TEXT
                Return cancelDialog()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
