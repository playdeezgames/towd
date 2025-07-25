Imports towd.business

Friend Class DeedsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const AVAILABLE_TEXT = "Available..."
    Const DONE_TEXT = "Done..."
    Const ALL_TEXT = "All..."

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({
                NEVER_MIND_TEXT,
                AVAILABLE_TEXT,
                DONE_TEXT,
                ALL_TEXT})
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Deeds")
    End Function

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim character = context.World.Avatar
        Select Case choice
            Case AVAILABLE_TEXT
                Return Task.FromResult(Of IUIDialog)(New FilteredDeedsUIDialog(context, "Available Deeds", Function(deed) character.IsAvailable(deed), MakeCopy))
            Case DONE_TEXT
                Return Task.FromResult(Of IUIDialog)(New FilteredDeedsUIDialog(context, "Done Deeds", Function(deed) character.HasDone(deed), MakeCopy))
            Case ALL_TEXT
                Return Task.FromResult(Of IUIDialog)(New FilteredDeedsUIDialog(context, "All Deeds", Function(deed) True, MakeCopy))
            Case NEVER_MIND_TEXT
                Return Task.FromResult(cancelDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New DeedsUIDialog(context, cancelDialog))
    End Function
End Class
