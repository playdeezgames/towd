Imports towd.data

Friend Class FurnaceRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Furnace, 1)
        SetItemTypeInput(data.ItemType.Brick, 8)
        SetRequiredLocationType(LocationType.Dirt)
        SetRequiredLocationType(LocationType.Grass)
        SetBuildsLocationType(LocationType.Furnace)
    End Sub
End Class
