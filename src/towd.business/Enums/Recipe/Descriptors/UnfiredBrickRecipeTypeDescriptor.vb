Imports towd.data

Friend Class UnfiredBrickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor
    Public Sub New()
        MyBase.New(RecipeType.UnfiredBrick, 1)
        SetInput(ItemType.PlantFiber, 1)
        SetInput(ItemType.Clay, 1)
        SetOutput(ItemType.UnfiredBrick, 1)
    End Sub
End Class
