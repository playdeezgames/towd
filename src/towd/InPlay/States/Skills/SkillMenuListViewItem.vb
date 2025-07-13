Imports towd.business

Public Class SkillMenuListViewItem
    Sub New(skillType As ISkillType, character As ICharacter)
        Me.SkillType = skillType
        Me.Character = character
    End Sub

    Public ReadOnly Property SkillType As ISkillType
    Private ReadOnly Character As ICharacter

    Public Overrides Function ToString() As String
        Return $"{SkillType}({SkillType.GetDescription(Character)})"
    End Function
End Class
