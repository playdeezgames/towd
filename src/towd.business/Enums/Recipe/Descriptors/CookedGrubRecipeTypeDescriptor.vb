Imports towd.data

Friend Class CookedGrubRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookedGrub, 1)
        SetItemTypeInput(ItemType.Grub, 1)
        SetItemTypeInput(ItemType.SharpStick, 1)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetItemTypeOutputGenerator(ItemType.SharpStick, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.CookedGrub, New FixedGenerator(1))
        SetRequiredLocationType(LocationType.CookingFire)
    End Sub
End Class
