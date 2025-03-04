Imports towd.data

Friend Class RawFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.RawFishFilet)
        SetInput(ItemType.Knife, 1)
        SetInput(ItemType.RawFish, 1)
        SetInputDurability(ItemType.Knife, 1)
        SetOutput(ItemType.Knife, 1)
        SetOutput(ItemType.FishGuts, 1)
        SetOutput(ItemType.FishHead, 1)
        SetOutput(ItemType.RawFishFilet, 1)
    End Sub
End Class
