Imports towd.data

Public MustInherit Class SkillTypeDescriptor
    Implements ISkillType

    Sub New(
           skillType As String,
           name As String,
           statisticType As String,
           maximum As Integer,
           description As String, Optional requiredDeeds As IEnumerable(Of IDeed) = Nothing)
        Me.SkillType = skillType
        Me.Name = name
        Me.StatisticType = statisticType
        Me.Description = description
        Me.maximum = maximum
        Me.RequiredDeeds = If(requiredDeeds, Array.Empty(Of IDeed))
    End Sub
    Public ReadOnly Property SkillType As String Implements ISkillType.SkillType
    Public ReadOnly Property Name As String Implements ISkillType.Name
    Public ReadOnly Property StatisticType As String Implements ISkillType.StatisticType
    Public ReadOnly Property Description As String Implements ISkillType.Description
    Public ReadOnly Property RequiredDeeds As IEnumerable(Of IDeed) Implements ISkillType.RequiredDeeds

    Private ReadOnly maximum As Integer
    Protected MustOverride Function GetAdvancementCost(character As ICharacter) As Integer
    Public Overrides Function ToString() As String
        Return Name
    End Function
    Public Function CanAdvance(character As ICharacter) As Boolean Implements ISkillType.CanAdvance
        Dim advancementCost = GetAdvancementCost(character)
        Dim xp = character.GetStatistic(business.StatisticType.XP)
        Dim hasAllDeeds = RequiredDeeds.All(Function(x) character.HasDone(x))
        Return character.GetStatistic(StatisticType) < maximum AndAlso xp >= advancementCost AndAlso hasAllDeeds
    End Function

    Public Function Advance(character As ICharacter) As Boolean Implements ISkillType.Advance
        Dim advancementCost = GetAdvancementCost(character)
        Dim xp = character.GetStatistic(business.StatisticType.XP)
        If Not CanAdvance(character) Then
            If character.GetStatistic(StatisticType) >= maximum Then
                character.AppendMessage(
                    $"Yer at maximum level {StatisticType.ToStatisticTypeDescriptor.Name}!")
                Return False
            End If
            If character.GetStatistic(business.StatisticType.XP) < advancementCost Then
                character.AppendMessage(
                    $"You need {advancementCost} XP to advance yer {StatisticType.ToStatisticTypeDescriptor.Name}!",
                    $"Alas, you have only {xp} XP.")
            End If
            Dim missingDeeds = RequiredDeeds.Where(Function(x) Not character.HasDone(x))
            For Each missingDeed In missingDeeds
                character.AppendMessage($"You needs to have done: {missingDeed.Name} to advance yer {StatisticType.ToStatisticTypeDescriptor.Name}.")
            Next
            Return False
        End If
        character.AppendMessage($"-{advancementCost} XP", $"+1 {StatisticType.ToStatisticTypeDescriptor.Name}")
        character.ChangeStatistic(business.StatisticType.XP, -advancementCost)
        character.ChangeStatistic(StatisticType, 1)
        Return True
    End Function

    Public Function GetDescription(character As ICharacter) As String Implements ISkillType.GetDescription
        Dim currentSkill = character.GetStatistic(StatisticType)
        If character.GetStatistic(StatisticType) >= maximum Then
            Return $"Current Skill: {currentSkill}[Max Level]"
        Else
            Dim missingDeeds = RequiredDeeds.Where(Function(x) Not character.HasDone(x))
            If missingDeeds.Any Then
                Return $"Requires {String.Join(", ", missingDeeds.Select(Function(x) x.Name))}"
            End If
            Return $"Current Skill: {currentSkill}, Advancement Cost: {GetAdvancementCost(character)} XP"
        End If
    End Function
End Class
