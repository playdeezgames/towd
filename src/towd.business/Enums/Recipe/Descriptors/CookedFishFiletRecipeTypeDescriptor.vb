Imports towd.data

Friend Class CookedFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookedFishFilet, 1)
        SetItemTypeInput(ItemType.RawFishFilet, 1)
        SetItemTypeInput(ItemType.SharpStick, 1)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetItemTypeOutputGenerator(ItemType.SharpStick, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.CookedFishFilet, New FixedGenerator(1))
        SetRequiredLocationType(LocationType.CookingFire)
    End Sub
End Class
