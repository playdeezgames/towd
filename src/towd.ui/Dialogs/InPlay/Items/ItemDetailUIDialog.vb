Imports towd.business

Friend Class ItemDetailUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly item As business.IItem
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext(Of IWorld), item As business.IItem, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.item = item
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return Array.Empty(Of (String, String, Boolean))
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
        Return result
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return $"{item.EntityType.Name}(#{item.Id})"
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return cancelDialog()
    End Function
End Class
