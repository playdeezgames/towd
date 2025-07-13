Friend Class MessageUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext

    Public Sub New(context As IUIContext)
        Me.context = context
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return context.World.Avatar.CurrentMessage
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {"Ok"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return String.Empty
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        context.World.Avatar.DismissMessage()
        Return NeutralUIDialog.DetermineInPlayDialog(context)
    End Function
End Class
