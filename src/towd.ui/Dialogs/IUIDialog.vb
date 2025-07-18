Public Interface IUIDialog
    Function GetLines() As IEnumerable(Of (String, String, Boolean))
    Function GetChoices() As IEnumerable(Of String)
    ReadOnly Property Prompt As String
    Function Choose(choice As String) As IUIDialog
End Interface
