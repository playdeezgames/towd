Imports towd.data

Public MustInherit Class AchievementTypeDescriptor
    Implements IAchievementType
    Sub New(achievementType As data.AchievementType, name As String)
        Me.AchievementType = achievementType
        Me.Name = name
    End Sub

    Public ReadOnly Property AchievementType As AchievementType Implements IAchievementType.AchievementType

    Public ReadOnly Property Name As String Implements IAchievementType.Name
    Public Sub Achieve(character As ICharacter) Implements IAchievementType.Achieve
        character.AppendMessage($"You have achieved ""{Name}""")
        OnAchieve(character)
    End Sub
    Protected MustOverride Sub OnAchieve(character As ICharacter)
    Public MustOverride Function IsAvailable(character As ICharacter) As Boolean Implements IAchievementType.IsAvailable
    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public MustOverride Function HasAchieved(character As ICharacter) As Boolean Implements IAchievementType.HasAchieved
End Class
