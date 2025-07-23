Imports System.Runtime.CompilerServices

Public Module Deeds
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of String, IDeed) =
        New List(Of IDeed) From
        {
            New CraftDeedDescriptor(business.Deed.CraftOnce, "Craft once", 1, 1, Array.Empty(Of String)()),
            New CraftDeedDescriptor(business.Deed.CraftTenTimes, "Craft ten times", 10, 1, {business.Deed.CraftOnce}),
            New CraftDeedDescriptor(business.Deed.CraftHundredTimes, "Craft a hundred times", 100, 1, {business.Deed.CraftTenTimes}),
            New CraftDeedDescriptor(business.Deed.CraftThousandTimes, "Craft a thousand times", 1000, 1, {business.Deed.CraftHundredTimes}),
            New ForageDeedDescriptor(business.Deed.ForageOnce, "Forage once", 1, 1, Array.Empty(Of String)()),
            New ForageDeedDescriptor(business.Deed.ForageTenTimes, "Forage ten times", 10, 1, {business.Deed.ForageOnce}),
            New ForageDeedDescriptor(business.Deed.ForageHundredTimes, "Forage a hundred times", 100, 1, {business.Deed.ForageTenTimes}),
            New ForageDeedDescriptor(business.Deed.ForageThousandTimes, "Forage a thousand times", 1000, 1, {business.Deed.ForageHundredTimes}),
            New MoveDeedDescriptor(business.Deed.MoveOnce, "Move once", 1, 1, Array.Empty(Of String)()),
            New MoveDeedDescriptor(business.Deed.MoveTenTimes, "Move ten times", 10, 1, {business.Deed.MoveOnce}),
            New MoveDeedDescriptor(business.Deed.MoveHundredTimes, "Move a hundred times", 100, 1, {business.Deed.MoveTenTimes}),
            New MoveDeedDescriptor(business.Deed.MoveThousandTimes, "Move a thousand times", 1000, 1, {business.Deed.MoveHundredTimes}),
            New BuildDeedDescriptor(business.Deed.BuildOnce, "Build once", 1, 1, Array.Empty(Of String)()),
            New BuildDeedDescriptor(business.Deed.BuildTenTimes, "Build ten times", 10, 1, {business.Deed.BuildOnce}),
            New BuildDeedDescriptor(business.Deed.BuildHundredTimes, "Build a hundred times", 100, 1, {business.Deed.BuildTenTimes}),
            New BuildDeedDescriptor(business.Deed.BuildThousandTimes, "Build a thousand times", 1000, 1, {business.Deed.BuildHundredTimes}),
            New QuestDeedDescriptor(business.Deed.CompleteKnapperQuest, "Knapping training", "Learn how to knap.")
        }.ToDictionary(Function(x) x.Deed, Function(x) x)
    <Extension>
    Public Function ToDeedDescriptor(deed As String) As IDeed
        Return Descriptors(deed)
    End Function
End Module
