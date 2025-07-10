Friend Class ForagingSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(business.SkillType.Foraging, "Foraging", business.StatisticType.ForagingSkill, 5, "Master the art of scavenging in the Tomb of Woeful Doom’s bleak sprawl.
Hunt for plant fibers, grubs, or questionable berries—your foraging skill decides if you eat or gag.
Higher levels turn trash into treasure; low rolls might just dig up despair.")
    End Sub
    Protected Overrides Function GetAdvancementCost(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType)
    End Function
End Class
