Imports towd.business

Friend Class DialogUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly dialog As IDialog

    Public Sub New(context As IUIContext(Of IWorld), dialog As IDialog)
        Me.context = context
        Me.dialog = dialog
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return dialog.Lines.Select(Function(x) (Mood.Normal, x, True))
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Return dialog.Choices
    End Function

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return dialog.Prompt
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim nextDialog = dialog.Choose(choice)
        If nextDialog IsNot Nothing Then
            Return MessageUIDialog.DetermineMessageDialog(context, Function() New DialogUIDialog(context, nextDialog))
        End If
        Return NeutralUIDialog.DetermineInPlayDialog(context)
    End Function
End Class
