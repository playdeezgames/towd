Friend Class SkillsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const ADVANCEABLE_TEXT = "Advanceable"
    Const ALL_TEXT = "All"

    Public Sub New(context As IUIContext, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {
                NEVER_MIND_TEXT,
                ADVANCEABLE_TEXT,
                ALL_TEXT
                }
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Skills"
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Dim character = context.World.Avatar
        Select Case choice
            Case NEVER_MIND_TEXT
                Return (Nothing, cancelDialog())
            Case ADVANCEABLE_TEXT
                Return (Nothing, New FilteredSkillsUIDialog(context, "All Skills", Function(skill) character.CanAdvance(skill), Function() Me))
            Case ALL_TEXT
                Return (Nothing, New FilteredSkillsUIDialog(context, "All Skills", Function(skill) True, Function() Me))
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
