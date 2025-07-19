Public Interface IUIDialog
    Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))
    Function GetChoices() As Task(Of IEnumerable(Of String))
    Function GetPrompt() As String
    Function Choose(choice As String) As IUIDialog
End Interface
