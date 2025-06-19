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
        Dim foragingSkill = character.GetStatistic(data.StatisticType.ForagingSkill)
        Dim xp = character.GetStatistic(data.StatisticType.XP)
        If Not CanAdvance(character) Then
            character.AddMessage(
                $"You need {foragingSkill} XP to advance yer Foraging Skill!",
                $"Alas, you have only {xp} XP.")
            Return False
        End If
        character.AddMessage($"-{foragingSkill} XP", $"+1 Foraging Skill")
        character.ChangeStatistic(data.StatisticType.XP, -foragingSkill)
        character.ChangeStatistic(data.StatisticType.ForagingSkill, 1)
        Return True
    End Function

    Public Overrides Function GetDescription(character As ICharacter) As String
        Dim foragingSkill = character.GetStatistic(data.StatisticType.ForagingSkill)
        Return $"Current Skill: {foragingSkill}, Advancement Cost: {foragingSkill} XP"
    End Function
End Class
