Public Class MessageBoxUIDialog
    Implements IUIDialog
    Sub New(prompt As String, lines As String(), nextDialog As Func(Of IUIDialog))
        Me.Prompt = prompt
        Me.Lines = lines
        Me.nextDialog = nextDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
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
