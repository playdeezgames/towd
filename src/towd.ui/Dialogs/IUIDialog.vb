Public Interface IUIDialog
    Function GetLines() As IEnumerable(Of (String, String, Boolean))
    Function GetChoices() As IEnumerable(Of String)
    Function GetPrompt() As String
    Function Choose(choice As String) As IUIDialog
End Interface
