Imports towd.data

Friend Class UnfiredBrickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor
    Public Sub New()
        MyBase.New(RecipeType.UnfiredBrick, 1)
        SetItemTypeInput(ItemType.PlantFiber, 1)
        SetItemTypeInput(ItemType.Clay, 1)
        SetItemTypeOutput(ItemType.UnfiredBrick, 1)
    End Sub
End Class
