Imports towd.business

Friend Class ItemStackUIDialog
    Implements IUIDialog

    Private context As IUIContext(Of IWorld)
    Private ReadOnly itemStack As business.IItemStack
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private ReadOnly table As IReadOnlyDictionary(Of String, IItem)

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Sub New(context As IUIContext(Of IWorld), itemStack As business.IItemStack, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.itemStack = itemStack
        Me.cancelDialog = cancelDialog
        table = context.World.Avatar.GetItemsOfType(itemStack.ItemType).ToDictionary(Function(x) $"{x.EntityType.Describe(x)}(#{x.Id})", Function(x) x)
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(Array.Empty(Of UIDialogLine))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
        result.AddRange(table.Keys)
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(itemStack.ToString())
    End Function

    Public Function Choose(choice As String, parameters As IReadOnlyDictionary(Of String, String)) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim item As IItem = Nothing
        If table.TryGetValue(choice, item) Then
            Return Task.FromResult(Of IUIDialog)(New ItemDetailUIDialog(context, item, MakeCopy))
        End If
        Return Task.FromResult(cancelDialog())
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New ItemStackUIDialog(context, itemStack, cancelDialog))
    End Function
End Class
