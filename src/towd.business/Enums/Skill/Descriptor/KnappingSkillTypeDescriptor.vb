Friend Class KnappingSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(business.SkillType.Knapping, "Knapping", business.StatisticType.KnappingSkill, 1, "Unleash the ancient craft of knapping in the Tomb of Woeful Doom’s unforgiving wilds.
Shape rocks into sharp blades or tools with each precise strike—your skill determines a weapon or a pile of rubble.
Higher levels forge deadly edges; low rolls might just bruise your pride.")
    End Sub

    Protected Overrides Function GetAdvancementCost(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType) + 1
    End Function
End Class
