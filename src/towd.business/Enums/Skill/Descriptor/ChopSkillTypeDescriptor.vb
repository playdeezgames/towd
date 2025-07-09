Friend Class ChopSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(data.SkillType.Chop, "Chop", business.StatisticType.ChopSkill, 5, "The ""Chop"" skill lets you hack through wood, branches, and the occasional unlucky sapling with brutal efficiency, turning nature’s bounty into crafting materials or kindling for your pathetic campfire. 
Master it to fell trees faster, gather more resources, and maybe even intimidate some wildlife with your sheer axe-wielding nihilism. 
Higher levels mean less sweat, more splinters, and a vague sense of dominance over the forest’s feeble attempts to survive your wrath.")
    End Sub
    Protected Overrides Function GetAdvancementCost(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType)
    End Function
End Class
