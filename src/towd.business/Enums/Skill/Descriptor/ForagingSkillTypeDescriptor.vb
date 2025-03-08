Friend Class ForagingSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(data.SkillType.Foraging, "Foraging", data.StatisticType.ForagingSkill)
    End Sub

    Public Overrides Function CanAdvance(character As ICharacter) As Boolean
        Return False
    End Function
End Class
