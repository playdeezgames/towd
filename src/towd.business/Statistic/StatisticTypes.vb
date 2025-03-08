Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports towd.data

Public Module StatisticTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of StatisticType, IStatisticType) =
        New List(Of IStatisticType) From
        {
            New StatisticTypeDescriptor(StatisticType.Chopping, "Chopping"),
            New StatisticTypeDescriptor(StatisticType.Satiety, "Satiety"),
            New StatisticTypeDescriptor(StatisticType.Health, "Health"),
            New StatisticTypeDescriptor(StatisticType.Durability, "Durability"),
            New StatisticTypeDescriptor(StatisticType.Fuel, "Fuel"),
            New StatisticTypeDescriptor(StatisticType.Foraging, "Foraging"),
            New StatisticTypeDescriptor(StatisticType.Digging, "Digging"),
            New StatisticTypeDescriptor(StatisticType.Fishing, "Fishing"),
            New StatisticTypeDescriptor(StatisticType.LastVerb, "Last Verb"),
            New StatisticTypeDescriptor(StatisticType.CurrentItemType, "Current Item Type"),
            New StatisticTypeDescriptor(StatisticType.LastRecipe, "Last Recipe"),
            New StatisticTypeDescriptor(StatisticType.Steps, "Steps"),
            New StatisticTypeDescriptor(StatisticType.XP, "XP")
        }.ToDictionary(Function(x) x.StatisticType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(statisticType As StatisticType) As IStatisticType
        Return Descriptors(statisticType)
    End Function
End Module
