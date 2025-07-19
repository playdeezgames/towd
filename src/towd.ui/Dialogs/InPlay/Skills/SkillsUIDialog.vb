Imports towd.business

Friend Class SkillsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const ADVANCEABLE_TEXT = "Advanceable"
    Const ALL_TEXT = "All"

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoices() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoices
        Return Task.FromResult(Of IEnumerable(Of String))({
                NEVER_MIND_TEXT,
                ADVANCEABLE_TEXT,
                ALL_TEXT
                })
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return "Skills"
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim character = context.World.Avatar
        Select Case choice
            Case NEVER_MIND_TEXT
                Return cancelDialog()
            Case ADVANCEABLE_TEXT
                Return New FilteredSkillsUIDialog(context, "Advanceable Skills", Function(skill) character.CanAdvance(skill), Function() Me)
            Case ALL_TEXT
                Return New FilteredSkillsUIDialog(context, "All Skills", Function(skill) True, Function() Me)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
