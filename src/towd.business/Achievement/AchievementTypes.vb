Imports System.Runtime.CompilerServices

Public Module AchievementTypes
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of data.AchievementType, IAchievementType) =
        New List(Of IAchievementType) From
        {
            New MoveOnceAchievementTypeDescriptor()
        }.ToDictionary(Function(x) x.AchievementType, Function(x) x)
    <Extension>
    Public Function ToDescriptor(achievementType As data.AchievementType) As IAchievementType
        Return Descriptors(achievementType)
    End Function
End Module
