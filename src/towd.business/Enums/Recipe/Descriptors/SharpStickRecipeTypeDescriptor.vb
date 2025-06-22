Imports towd.data

Friend Class SharpStickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.SharpStick, 1)
        SetInput(ItemType.Stick, 1)
        SetInput(ItemType.Hatchet, 1)
        SetItemTypeOutput(ItemType.Hatchet, 1)
        SetInputDurability(ItemType.Hatchet, 1)
        SetItemTypeOutput(ItemType.SharpStick, 1)
    End Sub
End Class
