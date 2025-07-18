Imports towd.business

Friend Class InventoryUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private table As IReadOnlyDictionary(Of String, IItemStack)

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table = context.World.Avatar.ItemStacks.ToDictionary(Function(x) x.ToString(), Function(x) x)
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
        Return "Inventory"
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim itemStack As IItemStack = Nothing
        If table.TryGetValue(choice, itemStack) Then
            Return New ItemStackUIDialog(context, itemStack, Function() New InventoryUIDialog(context, cancelDialog))
        End If
        Return cancelDialog()
    End Function
End Class
