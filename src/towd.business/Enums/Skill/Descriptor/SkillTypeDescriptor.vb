Imports towd.data

Public MustInherit Class SkillTypeDescriptor
    Implements ISkillType

    Sub New(skillType As SkillType, name As String, statisticType As String, maximum As Integer, description As String)
        Me.SkillType = skillType
        Me.Name = name
        Me.StatisticType = statisticType
        Me.Description = description
        Me.maximum = maximum
    End Sub
    Public ReadOnly Property SkillType As SkillType Implements ISkillType.SkillType
    Public ReadOnly Property Name As String Implements ISkillType.Name
    Public ReadOnly Property StatisticType As String Implements ISkillType.StatisticType
    Public ReadOnly Property Description As String Implements ISkillType.Description
    Private ReadOnly maximum As Integer
    Protected MustOverride Function GetAdvancementCost(character As ICharacter) As Integer
    Public Overrides Function ToString() As String
        Return Name
    End Function
    Public Function CanAdvance(character As ICharacter) As Boolean Implements ISkillType.CanAdvance
        Dim advancementCost = GetAdvancementCost(character)
        Dim xp = character.GetStatistic(business.StatisticType.XP)
        Return character.GetStatistic(StatisticType) < maximum AndAlso xp >= advancementCost
    End Function

    Public Function Advance(character As ICharacter) As Boolean Implements ISkillType.Advance
        Dim advancementCost = GetAdvancementCost(character)
        Dim xp = character.GetStatistic(business.StatisticType.XP)
        If Not CanAdvance(character) Then
            If character.GetStatistic(StatisticType) >= maximum Then
                character.AddMessage(
                    $"Yer at maximum level {StatisticType.ToStatisticTypeDescriptor.Name}!")
            Else
                character.AddMessage(
                    $"You need {advancementCost} XP to advance yer {StatisticType.ToStatisticTypeDescriptor.Name}!",
                    $"Alas, you have only {xp} XP.")
            End If
            Return False
        End If
        character.AddMessage($"-{advancementCost} XP", $"+1 {StatisticType.ToStatisticTypeDescriptor.Name}")
        character.ChangeStatistic(business.StatisticType.XP, -advancementCost)
        character.ChangeStatistic(StatisticType, 1)
        Return True
    End Function

    Public Function GetDescription(character As ICharacter) As String Implements ISkillType.GetDescription
        Dim currentSkill = character.GetStatistic(StatisticType)
        If character.GetStatistic(StatisticType) >= maximum Then
            Return $"Current Skill: {currentSkill}[Max Level]"
        Else
            Return $"Current Skill: {currentSkill}, Advancement Cost: {GetAdvancementCost(character)} XP"
        End If
    End Function
End Class
