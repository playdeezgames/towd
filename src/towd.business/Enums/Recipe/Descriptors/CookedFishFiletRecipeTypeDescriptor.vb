Imports towd.data

Friend Class CookedFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookedFishFilet, 1)
        SetItemTypeInput(ItemType.RawFishFilet, 1)
        SetItemTypeInput(ItemType.SharpStick, 1)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetItemTypeOutput(ItemType.SharpStick, 1)
        SetItemTypeOutput(ItemType.CookedFishFilet, 1)
        SetRequiredLocationType(LocationType.CookingFire)
    End Sub
End Class
