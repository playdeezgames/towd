Imports towd.data

Public MustInherit Class SkillTypeDescriptor
    Implements ISkillType

    Sub New(skillTYpe As SkillType, name As String, statisticType As StatisticType, description As String)
        Me.SkillType = skillTYpe
        Me.Name = name
        Me.StatisticType = statisticType
        Me.Description = description
    End Sub
    Public ReadOnly Property SkillType As SkillType Implements ISkillType.SkillType
    Public ReadOnly Property Name As String Implements ISkillType.Name
    Public ReadOnly Property StatisticType As StatisticType Implements ISkillType.StatisticType
    Public ReadOnly Property Description As String Implements ISkillType.Description
    Protected MustOverride Function GetAdvancementCost(character As ICharacter) As Integer
    Public Overrides Function ToString() As String
        Return Name
    End Function
    Public Function CanAdvance(character As ICharacter) As Boolean Implements ISkillType.CanAdvance
        Dim advancementCost = GetAdvancementCost(character)
        Dim xp = character.GetStatistic(data.StatisticType.XP)
        Return xp >= advancementCost
    End Function

    Public Function Advance(character As ICharacter) As Boolean Implements ISkillType.Advance
        Dim advancementCost = GetAdvancementCost(character)
        Dim xp = character.GetStatistic(data.StatisticType.XP)
        If Not CanAdvance(character) Then
            character.AddMessage(
                $"You need {advancementCost} XP to advance yer {StatisticType.ToDescriptor.Name}!",
                $"Alas, you have only {xp} XP.")
            Return False
        End If
        character.AddMessage($"-{advancementCost} XP", $"+1 {StatisticType.ToDescriptor.Name}")
        character.ChangeStatistic(data.StatisticType.XP, -advancementCost)
        character.ChangeStatistic(StatisticType, 1)
        Return True
    End Function

    Public Function GetDescription(character As ICharacter) As String Implements ISkillType.GetDescription
        Dim currentSkill = character.GetStatistic(StatisticType)
        Return $"Current Skill: {currentSkill}, Advancement Cost: {GetAdvancementCost(character)} XP"
    End Function
End Class
