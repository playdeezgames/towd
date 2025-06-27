Imports System.Runtime.CompilerServices
Imports towd.data

Public Module StatisticTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of StatisticType, IStatisticType) =
        New List(Of IStatisticType) From
        {
            New StatisticTypeDescriptor(StatisticType.BuildCounter, "BuildCounter"),
            New StatisticTypeDescriptor(StatisticType.DigSkill, "Dig Skill"),
            New StatisticTypeDescriptor(StatisticType.Digging, "Digging"),
            New StatisticTypeDescriptor(StatisticType.ChopSkill, "Chop Skill"),
            New StatisticTypeDescriptor(StatisticType.Chopping, "Chopping"),
            New StatisticTypeDescriptor(StatisticType.CraftCounter, "Crafting"),
            New StatisticTypeDescriptor(StatisticType.CurrentItemType, "Current Item Type"),
            New StatisticTypeDescriptor(StatisticType.Durability, "Durability"),
            New StatisticTypeDescriptor(StatisticType.Fishing, "Fishing"),
            New StatisticTypeDescriptor(StatisticType.ForagingCounter, "Foraging"),
            New StatisticTypeDescriptor(StatisticType.FishSkill, "Fish Skill"),
            New StatisticTypeDescriptor(StatisticType.ForagingSkill, "Foraging Skill"),
            New StatisticTypeDescriptor(StatisticType.Fuel, "Fuel"),
            New StatisticTypeDescriptor(StatisticType.Health, "Health"),
            New StatisticTypeDescriptor(StatisticType.KnappingSkill, "Knapping Skill"),
            New StatisticTypeDescriptor(StatisticType.LastVerb, "Last Verb"),
            New StatisticTypeDescriptor(StatisticType.Satiety, "Satiety"),
            New StatisticTypeDescriptor(StatisticType.Steps, "Steps"),
            New StatisticTypeDescriptor(StatisticType.XP, "XP")
        }.ToDictionary(Function(x) x.StatisticType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(statisticType As StatisticType) As IStatisticType
        Return Descriptors(statisticType)
    End Function
End Module
