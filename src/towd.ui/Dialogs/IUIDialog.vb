Public Interface IUIDialog
    ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean))
    ReadOnly Property Choices As IEnumerable(Of String)
    ReadOnly Property Prompt As String
    Function Choose(choice As String) As IUIDialog
End Interface
