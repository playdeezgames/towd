Public Interface IDialog
    ReadOnly Property Lines As IEnumerable(Of String)
    ReadOnly Property Choices As IEnumerable(Of String)
    ReadOnly Property Prompt As String
    Function Choose(choice As String) As IDialog
End Interface