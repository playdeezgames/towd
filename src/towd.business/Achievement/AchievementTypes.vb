Imports System.Runtime.CompilerServices

Public Module AchievementTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of data.AchievementType, IAchievementType) =
        New List(Of IAchievementType) From
        {
            New MoveAchievementTypeDescriptor(data.AchievementType.MoveOnce, "Move once", 1, Array.Empty(Of data.AchievementType)()),
            New MoveAchievementTypeDescriptor(data.AchievementType.MoveTenTimes, "Move ten times", 10, {data.AchievementType.MoveOnce}),
            New MoveAchievementTypeDescriptor(data.AchievementType.MoveHundredTimes, "Move a hundred times", 100, {data.AchievementType.MoveTenTimes}),
            New MoveAchievementTypeDescriptor(data.AchievementType.MoveThousandTimes, "Move a thousand times", 1000, {data.AchievementType.MoveHundredTimes})
        }.ToDictionary(Function(x) x.AchievementType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(achievementType As data.AchievementType) As IAchievementType
        Return Descriptors(achievementType)
    End Function
End Module
