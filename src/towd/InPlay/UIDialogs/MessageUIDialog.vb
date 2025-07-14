Friend Class MessageUIDialog
    Implements IUIDialog

    Friend Shared Function DetermineMessageDialog(context As IUIContext, nextDialog As Func(Of IUIDialog)) As IUIDialog
        If context.World.Avatar?.HasMessages Then
            Return New MessageUIDialog(context, nextDialog)
        End If
        Return nextDialog()
    End Function

    Private ReadOnly context As IUIContext
    Private ReadOnly nextDialog As Func(Of IUIDialog)

    Public Sub New(context As IUIContext, nextDialog As Func(Of IUIDialog))
        Me.context = context
        Me.nextDialog = nextDialog
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
        If context.World.Avatar.HasMessages Then
            Return (Nothing, Me)
        End If
        Return (Nothing, nextDialog())
    End Function
End Class
