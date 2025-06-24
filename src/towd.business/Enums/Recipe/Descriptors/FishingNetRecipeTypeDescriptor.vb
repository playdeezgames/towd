Imports towd.data

Friend Class FishingNetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor
    Public Sub New()
        MyBase.New(RecipeType.FishingNet, 1)
        SetItemTypeInput(ItemType.Twine, 4)
        SetItemTypeOutput(ItemType.FishingNet, 1)
    End Sub
End Class
