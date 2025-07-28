Public Interface IUIDialog
    Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine))
    Function GetChoicesAsync() As Task(Of IEnumerable(Of String))
    Function GetPromptAsync() As Task(Of String)
    Function Choose(choice As String) As Task(Of IUIDialog)
    Function MakeCopy() As Func(Of IUIDialog)
End Interface
