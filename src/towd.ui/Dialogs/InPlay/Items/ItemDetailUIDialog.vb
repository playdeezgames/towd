Friend Class ItemDetailUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly item As business.IItem
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext, item As business.IItem, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.item = item
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of (String, String, Boolean))
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return $"{item.EntityType.Name}(#{item.Id})"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return cancelDialog()
    End Function
End Class
