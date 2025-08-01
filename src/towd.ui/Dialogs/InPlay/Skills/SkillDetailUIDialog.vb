﻿Imports towd.business

Friend Class SkillDetailUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly character As ICharacter
    Private ReadOnly skillType As business.ISkillType
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const ADVANCE_TEXT = "Advance"

    Public Sub New(context As IUIContext(Of IWorld), character As ICharacter, skillType As business.ISkillType, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.character = character
        Me.skillType = skillType
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))({New UIDialogLine(Mood.Normal, skillType.Description, True)})
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
        If skillType.CanAdvance(character) Then
            result.Add(ADVANCE_TEXT)
        End If
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult($"{skillType}({skillType.GetDescription(character)})")
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case ADVANCE_TEXT
                skillType.Advance(character)
                Return Task.FromResult(MessageUIDialog.DetermineMessageDialog(context, cancelDialog))
            Case NEVER_MIND_TEXT
                Return Task.FromResult(cancelDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New SkillDetailUIDialog(context, character, skillType, cancelDialog))
    End Function
End Class
