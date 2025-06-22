Imports towd.data

Friend Class FurnaceRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Furnace, 1)
        SetInput(data.ItemType.Brick, 8)
        SetRequiredLocation(LocationType.Dirt)
        SetRequiredLocation(LocationType.Grass)
        SetLocationTypeOutput(LocationType.Furnace)
    End Sub
End Class
