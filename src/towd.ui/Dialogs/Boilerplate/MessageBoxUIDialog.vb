Public Class MessageBoxUIDialog
    Implements IUIDialog
    Sub New(prompt As String, lines As (String, String, Boolean)(), nextDialog As Func(Of IUIDialog))
        Me.Prompt = prompt
        Me._Lines = lines
        Me.nextDialog = nextDialog
    End Sub

    Private _Lines As IEnumerable(Of (String, String, Boolean))

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return _Lines
    End Function

    Private ReadOnly nextDialog As Func(Of IUIDialog)

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Return {"Ok"}
    End Function

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return nextDialog()
    End Function
End Class
