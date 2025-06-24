Imports towd.data

Friend Class SharpStickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.SharpStick, 1)
        SetItemTypeInput(ItemType.Stick, 1)
        SetItemTypeInput(ItemType.Hatchet, 1)
        SetItemTypeOutputGenerator(ItemType.Hatchet, New FixedGenerator(1))
        SetItemTypeInputDurability(ItemType.Hatchet, 1)
        SetItemTypeOutputGenerator(ItemType.SharpStick, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
