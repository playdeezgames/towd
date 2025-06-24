Imports towd.data

Friend Class BrickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Brick, 1)
        SetItemTypeInput(ItemType.UnfiredBrick, 1)
        SetItemTypeOutput(ItemType.Brick, 1)
        SetRequiredLocationType(LocationType.CookingFire)
        SetRequiredLocationType(LocationType.Furnace)
    End Sub
End Class
