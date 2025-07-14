Imports towd.business

Friend Class SkillDetailUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly character As ICharacter
    Private ReadOnly skillType As business.ISkillType
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const ADVANCE_TEXT = "Advance"

    Public Sub New(context As IUIContext, character As ICharacter, skillType As business.ISkillType, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.character = character
        Me.skillType = skillType
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return {skillType.Description}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
            If skillType.CanAdvance(character) Then
                result.Add(ADVANCE_TEXT)
            End If
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return $"{skillType}({skillType.GetDescription(character)})"
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case ADVANCE_TEXT
                skillType.Advance(character)
                Return (Nothing, MessageUIDialog.DetermineMessageDialog(context, cancelDialog))
            Case NEVER_MIND_TEXT
                Return (Nothing, cancelDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
