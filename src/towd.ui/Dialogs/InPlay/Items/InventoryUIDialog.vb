Imports towd.business

Friend Class InventoryUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private table As IReadOnlyDictionary(Of String, IItemStack)

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table = context.World.Avatar.ItemStacks.ToDictionary(Function(x) x.ToString(), Function(x) x)
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
        Return Task.FromResult("Inventory")
    End Function

    Public Function Choose(choice As String, parameters As IReadOnlyDictionary(Of String, String)) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim itemStack As IItemStack = Nothing
        If table.TryGetValue(choice, itemStack) Then
            Return Task.FromResult(Of IUIDialog)(New ItemStackUIDialog(context, itemStack, MakeCopy))
        End If
        Return Task.FromResult(cancelDialog())
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New InventoryUIDialog(context, cancelDialog))
    End Function
End Class
