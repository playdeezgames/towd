Imports towd.data

Friend Class SharpStickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.SharpStick, 1)
        SetItemTypeInput(ItemType.Stick, 1)
        SetItemTypeInput(ItemType.Hatchet, 1)
        SetItemTypeOutput(ItemType.Hatchet, 1)
        SetItemTypeInputDurability(ItemType.Hatchet, 1)
        SetItemTypeOutput(ItemType.SharpStick, 1)
    End Sub
End Class
