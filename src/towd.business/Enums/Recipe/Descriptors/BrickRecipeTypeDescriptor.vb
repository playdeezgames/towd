Imports towd.data

Friend Class BrickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Brick, 1)
        SetInput(ItemType.UnfiredBrick, 1)
        SetItemTypeOutput(ItemType.Brick, 1)
        SetRequiredLocation(LocationType.CookingFire)
        SetRequiredLocation(LocationType.Furnace)
    End Sub
End Class
