Friend Class DigSkillTypeDescriptor
    Inherits SkillTypeDescriptor

    Public Sub New()
        MyBase.New(data.SkillType.Dig, "Dig", data.StatisticType.DigSkill, 5, "With a gritty determination and a complete disregard for manicured nails, the Dig skill lets you claw through the earth like a nihilistic badger. 
In grassy patches, you’ll unearth wriggling grubs—slimy, protein-packed morsels that laugh in the face of starvation. 
By a pond, your frantic scraping yields clumps of clay, perfect for crafting crude pottery or smearing on your face for that ""I’ve given up"" aesthetic. 
It’s dirty, it’s desperate, and it’s your ticket to surviving another miserable day in the Tomb of Woeful Doom.")
    End Sub
    Protected Overrides Function GetAdvancementCost(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType)
    End Function
End Class
