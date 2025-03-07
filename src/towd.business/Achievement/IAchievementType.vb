Public Interface IAchievementType
    ReadOnly Property AchievementType As data.AchievementType
    ReadOnly Property Name As String
    Sub Achieve(character As ICharacter)
    Function IsAvailable(character As ICharacter) As Boolean
    Function HasAchieved(character As ICharacter) As Boolean
End Interface
