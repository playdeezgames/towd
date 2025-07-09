Friend MustInherit Class StatefulDialog
    Implements IDialog
    Private _subdialog As IDialog

    Private ReadOnly Property Subdialog As IDialog
        Get
            If _subdialog Is Nothing Then
                _subdialog = CreateSubdialog()
            End If
            Return _subdialog
        End Get
    End Property

    Protected MustOverride Function CreateSubdialog() As IDialog

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return Subdialog.Lines
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return Subdialog.Choices
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return Subdialog.Prompt
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Dim nextDialog = Subdialog.Choose(choice)
        _subdialog = Nothing
        Return nextDialog
    End Function
End Class
