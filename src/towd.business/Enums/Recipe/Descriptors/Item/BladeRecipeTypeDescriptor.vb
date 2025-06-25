Imports towd.data

Friend Class BladeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Blade, 1)
        SetItemTypeInput(ItemType.SharpRock, 1)
        SetItemTypeInput(ItemType.Hammer, 1)
        SetItemTypeInputDurability(ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(ItemType.Hammer, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.Blade, New FixedGenerator(1))
        SetCharacterStatisticMinimum(StatisticType.KnappingSkill, 1)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
