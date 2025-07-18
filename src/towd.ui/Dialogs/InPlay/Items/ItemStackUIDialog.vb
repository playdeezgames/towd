Imports towd.business

Friend Class ItemStackUIDialog
    Implements IUIDialog

    Private context As IUIContext(Of IWorld)
    Private ReadOnly itemStack As business.IItemStack
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private ReadOnly table As IReadOnlyDictionary(Of String, IItem)

    Public Sub New(context As IUIContext(Of IWorld), itemStack As business.IItemStack, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.itemStack = itemStack
        Me.cancelDialog = cancelDialog
        table = context.World.Avatar.GetItemsOfType(itemStack.ItemType).ToDictionary(Function(x) $"{x.EntityType.Describe(x)}(#{x.Id})", Function(x) x)
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return Array.Empty(Of (String, String, Boolean))
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
        result.AddRange(table.Keys)
        Return result
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return itemStack.ToString()
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim item As IItem = Nothing
        If table.TryGetValue(choice, item) Then
            Return New ItemDetailUIDialog(context, item, Function() New ItemStackUIDialog(context, itemStack, cancelDialog))
        End If
        Return cancelDialog()
    End Function
End Class
