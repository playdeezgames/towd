Imports towd.business

Friend Class DialogUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly dialog As IDialog

    Public Sub New(context As IUIContext(Of IWorld), dialog As IDialog)
        Me.context = context
        Me.dialog = dialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return dialog.Lines.Select(Function(x) (Mood.Normal, x, True))
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return dialog.Choices
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return dialog.Prompt
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim nextDialog = dialog.Choose(choice)
        If nextDialog IsNot Nothing Then
            Return MessageUIDialog.DetermineMessageDialog(context, Function() New DialogUIDialog(context, nextDialog))
        End If
        Return NeutralUIDialog.DetermineInPlayDialog(context)
    End Function
End Class
