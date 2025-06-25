Friend Class DigSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(data.SkillType.Dig, "Dig", data.StatisticType.DigSkill, 5, "")
    End Sub
    Protected Overrides Function GetAdvancementCost(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType)
    End Function
End Class
