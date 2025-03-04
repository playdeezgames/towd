Imports towd.data

Friend Class FurnaceRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Furnace)
        SetInput(data.ItemType.Brick, 8)
        SetOutput(data.ItemType.Furnace, 1)
    End Sub
    Protected Overrides Function Precondition(Character As ICharacter) As Boolean
        Dim locationType = Character.Location.EntityType.LocationType
        Return locationType = data.LocationType.Dirt OrElse locationType = data.LocationType.Grass
    End Function
    Protected Overrides Sub Predicate(character As ICharacter)
        character.RemoveItemOfType(ItemType.Furnace.ToDescriptor)
        character.Location.EntityType = LocationType.Furnace.ToDescriptor
    End Sub
End Class
