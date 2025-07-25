﻿Public Interface IUIDialog
    Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))
    Function GetChoicesAsync() As Task(Of IEnumerable(Of String))
    Function GetPromptAsync() As Task(Of String)
    Function Choose(choice As String) As Task(Of IUIDialog)
    Function MakeCopy() As Func(Of IUIDialog)
End Interface
