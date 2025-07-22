Imports towd.data

Friend Class BladeVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Blade, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.SharpRock, 1)
        SetItemTypeInput(ItemType.Hammer, 1)
        SetItemTypeInputDurability(ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(ItemType.Hammer, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.Blade, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.RockFlake, New FixedCharacterWeightedGenerator(2))
        SetCharacterStatisticMinimum(StatisticType.KnappingSkill, 1)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("You know that a crafting system is too complex when you have to master knapping.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
