Imports towd.data

Friend Class PlankRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Plank, 1)
        SetInput(ItemType.Hatchet, 1)
        SetItemTypeOutput(ItemType.Hatchet, 1)
        SetInputDurability(ItemType.Hatchet, 3)
        SetInput(ItemType.Hammer, 1)
        SetItemTypeOutput(ItemType.Hammer, 1)
        SetInputDurability(ItemType.Hammer, 3)
        SetInput(ItemType.Log, 1)
        SetItemTypeOutput(ItemType.Plank, 4)
    End Sub
End Class
