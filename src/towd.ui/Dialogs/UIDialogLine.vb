Public Class UIDialogLine
    ReadOnly Property Mood As String
    ReadOnly Property Text As String
    ReadOnly Property EndsLine As Boolean
    Sub New(mood As String, text As String, endsLine As Boolean)
        Me.Mood = mood
        Me.Text = text
        Me.EndsLine = endsLine
    End Sub
End Class
