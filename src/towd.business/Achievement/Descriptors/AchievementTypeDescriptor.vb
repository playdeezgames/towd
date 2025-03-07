Imports towd.data

Public MustInherit Class AchievementTypeDescriptor
    Implements IAchievementType
    Private ReadOnly needed As HashSet(Of data.AchievementType)
    Sub New(achievementType As data.AchievementType, name As String, needed As data.AchievementType())
        Me.needed = New HashSet(Of data.AchievementType)(needed)
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
    Public Overridable Function IsAvailable(character As ICharacter) As Boolean Implements IAchievementType.IsAvailable
        Return needed.All(Function(x) character.HasAchieved(x.ToDescriptor))
    End Function
    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public MustOverride Function HasAchieved(character As ICharacter) As Boolean Implements IAchievementType.HasAchieved
End Class
