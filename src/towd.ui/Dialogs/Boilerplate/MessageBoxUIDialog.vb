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

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {"Ok"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return nextDialog()
    End Function
End Class
