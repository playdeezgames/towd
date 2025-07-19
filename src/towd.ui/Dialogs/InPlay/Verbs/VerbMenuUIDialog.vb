Imports towd.business

Friend Class VerbMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const AVAILABLE_TEXT = "Available"
    Const ALL_TEXT = "All"

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT,
                    AVAILABLE_TEXT,
                    ALL_TEXT
                }
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Verbs")
    End Function

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case ALL_TEXT
                Return Task.FromResult(Of IUIDialog)(New FilteredVerbUIDialog(context, "All Verbs", Function(verbType As IVerbType, character As ICharacter) True, Function() Me))
            Case AVAILABLE_TEXT
                Return Task.FromResult(Of IUIDialog)(New FilteredVerbUIDialog(context, "Available Verbs", Function(verbType As IVerbType, character As ICharacter) verbType.CanPerform(character), Function() Me))
            Case NEVER_MIND_TEXT
                Return Task.FromResult(cancelDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
