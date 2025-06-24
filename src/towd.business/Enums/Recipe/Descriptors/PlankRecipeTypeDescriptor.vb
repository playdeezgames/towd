Imports towd.data

Friend Class PlankRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Plank, 1)
        SetItemTypeInput(ItemType.Hatchet, 1)
        SetItemTypeOutputGenerator(ItemType.Hatchet, New FixedGenerator(1))
        SetItemTypeInputDurability(ItemType.Hatchet, 3)
        SetItemTypeInput(ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(ItemType.Hammer, New FixedGenerator(1))
        SetItemTypeInputDurability(ItemType.Hammer, 3)
        SetItemTypeInput(ItemType.Log, 1)
        SetItemTypeOutputGenerator(ItemType.Plank, New FixedGenerator(4))
    End Sub
End Class
