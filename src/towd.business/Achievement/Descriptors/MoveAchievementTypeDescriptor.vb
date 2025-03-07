Imports towd.data

Friend Class MoveAchievementTypeDescriptor
    Inherits AchievementTypeDescriptor
    Private ReadOnly stepCount As Integer

    Public Sub New(achievementType As AchievementType, name As String, stepCount As Integer, needed As data.AchievementType())
        MyBase.New(achievementType, name, needed)
        Me.stepCount = stepCount
    End Sub

    Protected Overrides Sub OnAchieve(character As ICharacter)
    End Sub

    Public Overrides Function IsAvailable(character As ICharacter) As Boolean
        Return MyBase.IsAvailable(character)
    End Function

    Public Overrides Function HasAchieved(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.Steps) >= stepCount
    End Function
End Class
