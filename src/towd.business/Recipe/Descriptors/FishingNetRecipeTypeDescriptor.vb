Imports towd.data

Friend Class FishingNetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor
    Public Sub New()
        MyBase.New(RecipeType.FishingNet)
        SetInput(ItemType.Twine, 4)
        SetOutput(ItemType.FishingNet, 1)
    End Sub
End Class
