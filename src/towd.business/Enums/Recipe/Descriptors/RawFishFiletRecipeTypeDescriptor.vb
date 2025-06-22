Imports towd.data

Friend Class RawFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.RawFishFilet, 1)
        SetInput(ItemType.Knife, 1)
        SetInput(ItemType.RawFish, 1)
        SetInputDurability(ItemType.Knife, 1)
        SetItemTypeOutput(ItemType.Knife, 1)
        SetItemTypeOutput(ItemType.FishGuts, 1)
        SetItemTypeOutput(ItemType.FishHead, 1)
        SetItemTypeOutput(ItemType.RawFishFilet, 1)
    End Sub
End Class
