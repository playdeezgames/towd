Imports towd.business

Friend Class InventoryUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private table As IReadOnlyDictionary(Of String, IItemStack)

    Public Sub New(context As IUIContext, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table = context.World.Avatar.ItemStacks.ToDictionary(Function(x) x.ToString(), Function(x) x)
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
            result.AddRange(table.Keys)
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Inventory"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim itemStack As IItemStack = Nothing
        If table.TryGetValue(choice, itemStack) Then
            Return New ItemStackUIDialog(context, itemStack, Function() New InventoryUIDialog(context, cancelDialog))
        End If
        Return cancelDialog()
    End Function
End Class
