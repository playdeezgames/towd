Imports towd.data

Friend Class CookedGrubRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookedGrub, 1)
        SetInput(ItemType.Grub, 1)
        SetInput(ItemType.SharpStick, 1)
        SetInputDurability(ItemType.SharpStick, 1)
        SetItemTypeOutput(ItemType.SharpStick, 1)
        SetItemTypeOutput(ItemType.CookedGrub, 1)
        SetRequiredLocation(LocationType.CookingFire)
    End Sub
End Class
