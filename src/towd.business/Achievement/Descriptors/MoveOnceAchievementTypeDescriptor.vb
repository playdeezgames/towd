Imports towd.data

Friend Class MoveOnceAchievementTypeDescriptor
    Inherits AchievementTypeDescriptor

    Public Sub New()
        MyBase.New(AchievementType.MoveOnce, "Move Once")
    End Sub

    Protected Overrides Sub OnAchieve(character As ICharacter)
    End Sub

    Public Overrides Function IsAvailable(character As ICharacter) As Boolean
        Return True
    End Function

    Public Overrides Function HasAchieved(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.Steps) >= 1
    End Function
End Class
