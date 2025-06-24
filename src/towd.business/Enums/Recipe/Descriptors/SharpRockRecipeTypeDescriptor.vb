Friend Class SharpRockRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.SharpRock, 1)
        SetItemTypeInput(data.ItemType.Rock, 1)
        SetItemTypeInput(data.ItemType.Hammer, 1)
        SetItemTypeOutput(data.ItemType.Hammer, 1)
        SetItemTypeOutput(data.ItemType.SharpRock, 1)
        SetItemTypeInputDurability(data.ItemType.Hammer, 1)
    End Sub
End Class
