Friend Class ConfirmUIDialog
    Implements IUIDialog

    Private ReadOnly caption As String
    Private ReadOnly confirmDialog As IUIDialog
    Private ReadOnly cancelDialog As IUIDialog

    Const YES_TEXT = "Yes"
    Const NO_TEXT = "No"

    Public Sub New(caption As String, confirmDialog As IUIDialog, cancelDialog As IUIDialog)
        Me.caption = caption
        Me.confirmDialog = confirmDialog
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of String)
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

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case YES_TEXT
                Return (Nothing, confirmDialog)
            Case NO_TEXT
                Return (Nothing, cancelDialog)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
