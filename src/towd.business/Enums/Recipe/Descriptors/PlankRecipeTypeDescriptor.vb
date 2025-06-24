Imports towd.data

Friend Class PlankRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Plank, 1)
        SetItemTypeInput(ItemType.Hatchet, 1)
        SetItemTypeOutput(ItemType.Hatchet, 1)
        SetItemTypeInputDurability(ItemType.Hatchet, 3)
        SetItemTypeInput(ItemType.Hammer, 1)
        SetItemTypeOutput(ItemType.Hammer, 1)
        SetItemTypeInputDurability(ItemType.Hammer, 3)
        SetItemTypeInput(ItemType.Log, 1)
        SetItemTypeOutput(ItemType.Plank, 4)
    End Sub
End Class
