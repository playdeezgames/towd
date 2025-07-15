Friend Class DeedDetailUIDialog
    Implements IUIDialog

    Private ReadOnly deed As business.IDeed
    Private ReadOnly cancelDialog As Func(Of IUIDialog)

    Public Sub New(deed As business.IDeed, cancelDialog As Func(Of IUIDialog))
        Me.deed = deed
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return {deed.Description}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {"Ok"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return deed.Name
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return cancelDialog()
    End Function
End Class
