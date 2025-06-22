Imports towd.data

Friend Class FurnaceRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Furnace, 1)
        SetInput(data.ItemType.Brick, 8)
        SetOutput(data.ItemType.Furnace, 1)
        SetRequiredLocation(LocationType.Dirt)
        SetRequiredLocation(LocationType.Grass)
    End Sub
    Protected Overrides Sub Predicate(character As ICharacter)
        character.RemoveItemOfType(ItemType.Furnace.ToDescriptor)
        character.Location.EntityType = LocationType.Furnace.ToDescriptor
    End Sub
End Class
