Imports towd.business

Friend Class DeadUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return Array.Empty(Of (String, String, Boolean))
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Return {"Ok"}
    End Function

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
