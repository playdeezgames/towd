Friend Class FishSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(data.SkillType.Fish, "Fish", business.StatisticType.FishSkill, 5, "With the Fish skill, you stand at the edge of a murky pond, clutching your tattered fishing net like it's the last shred of hope in this godforsaken wasteland. 
You cast the net with a mix of desperation and finesse, hoping to snag something edible from the slimy depths. 
Success means hauling in a wriggling raw fish—slippery, smelly, and just barely qualifying as food. 
Fail, and you’re left with a net full of pond scum and a bruised ego. Requires a fishing net, because flailing at the water with your hands is just sad.")
    End Sub
    Protected Overrides Function GetAdvancementCost(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType)
    End Function
End Class
