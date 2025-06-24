Imports towd.data

Friend Class RawFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.RawFishFilet, 1)
        SetItemTypeInput(ItemType.Knife, 1)
        SetItemTypeInput(ItemType.RawFish, 1)
        SetItemTypeInputDurability(ItemType.Knife, 1)
        SetItemTypeOutput(ItemType.Knife, 1)
        SetItemTypeOutput(ItemType.FishGuts, 1)
        SetItemTypeOutput(ItemType.FishHead, 1)
        SetItemTypeOutput(ItemType.RawFishFilet, 1)
    End Sub
End Class
