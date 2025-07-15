Friend Class DeadUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext

    Public Sub New(context As IUIContext)
        Me.context = context
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {"Ok"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Yer dead."
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        context.World.Abandon()
        Return New MainMenuUIDialog(context)
    End Function
End Class
