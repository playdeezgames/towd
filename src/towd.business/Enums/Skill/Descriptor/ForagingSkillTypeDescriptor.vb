Friend Class ForagingSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(data.SkillType.Foraging, "Foraging", data.StatisticType.ForagingSkill)
    End Sub

    Public Overrides Function CanAdvance(character As ICharacter) As Boolean
        Dim foragingSkill = character.GetStatistic(data.StatisticType.ForagingSkill)
        Dim xp = character.GetStatistic(data.StatisticType.XP)
        Return xp >= foragingSkill
    End Function

    Public Overrides Function Advance(character As ICharacter) As Boolean
        If Not CanAdvance(character) Then
            Return False
        End If
        Dim foragingSkill = character.GetStatistic(data.StatisticType.ForagingSkill)
        character.ChangeStatistic(data.StatisticType.XP, -foragingSkill)
        character.ChangeStatistic(data.StatisticType.ForagingSkill, 1)
        Return True
    End Function

    Public Overrides Function GetDescription(character As ICharacter) As String
        Dim foragingSkill = character.GetStatistic(data.StatisticType.ForagingSkill)
        Return $"XP Cost: {foragingSkill}"
    End Function
End Class
