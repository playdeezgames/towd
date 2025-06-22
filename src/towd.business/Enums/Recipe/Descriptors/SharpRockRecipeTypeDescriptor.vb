Friend Class SharpRockRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.SharpRock, 1)
        SetInput(data.ItemType.Rock, 1)
        SetInput(data.ItemType.Hammer, 1)
        SetItemTypeOutput(data.ItemType.Hammer, 1)
        SetItemTypeOutput(data.ItemType.SharpRock, 1)
        SetInputDurability(data.ItemType.Hammer, 1)
    End Sub
End Class
