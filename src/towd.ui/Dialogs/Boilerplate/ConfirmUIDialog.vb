Friend Class ConfirmUIDialog
    Implements IUIDialog

    Private ReadOnly caption As String
    Private ReadOnly confirmDialog As Func(Of IUIDialog)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)

    Const YES_TEXT = "Yes"
    Const NO_TEXT = "No"

    Public Sub New(caption As String, confirmDialog As Func(Of IUIDialog), cancelDialog As Func(Of IUIDialog))
        Me.caption = caption
        Me.confirmDialog = confirmDialog
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of (String, String, Boolean))
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {NO_TEXT, YES_TEXT}
        End Get
    End Property

    Private ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return caption
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Select Case choice
            Case YES_TEXT
                Return confirmDialog()
            Case NO_TEXT
                Return cancelDialog()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
