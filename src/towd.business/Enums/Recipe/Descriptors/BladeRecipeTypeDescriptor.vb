Imports towd.data

Friend Class BladeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Blade, 1)
        SetItemTypeInput(ItemType.SharpRock, 1)
        SetItemTypeInput(ItemType.Hammer, 1)
        SetItemTypeInputDurability(ItemType.Hammer, 1)
        SetItemTypeOutput(ItemType.Hammer, 1)
        SetItemTypeOutput(ItemType.Blade, 1)
        SetCharacterStatisticMinimum(StatisticType.KnappingSkill, 1)
    End Sub
End Class
