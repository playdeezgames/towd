Imports towd.business

Friend Class DeedsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const AVAILABLE_TEXT = "Available..."
    Const DONE_TEXT = "Done..."
    Const ALL_TEXT = "All..."

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of (String, String, Boolean))
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {
                NEVER_MIND_TEXT,
                AVAILABLE_TEXT,
                DONE_TEXT,
                ALL_TEXT}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Deeds"
        End Get
    End Property

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim character = context.World.Avatar
        Select Case choice
            Case AVAILABLE_TEXT
                Return New FilteredDeedsUIDialog(context, "Available Deeds", Function(deed) character.IsAvailable(deed), Function() Me)
            Case DONE_TEXT
                Return New FilteredDeedsUIDialog(context, "Done Deeds", Function(deed) character.HasDone(deed), Function() Me)
            Case ALL_TEXT
                Return New FilteredDeedsUIDialog(context, "All Deeds", Function(deed) True, Function() Me)
            Case NEVER_MIND_TEXT
                Return cancelDialog()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
