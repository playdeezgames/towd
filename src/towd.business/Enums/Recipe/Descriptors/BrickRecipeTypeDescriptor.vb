Imports towd.data

Friend Class BrickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Brick, 1)
        SetInput(ItemType.UnfiredBrick, 1)
        SetOutput(ItemType.Brick, 1)
    End Sub
    Protected Overrides Function Precondition(Character As ICharacter) As Boolean
        Dim locationType = Character.Location.EntityType.LocationType
        Return locationType = LocationType.CookingFire OrElse locationType = LocationType.Furnace
    End Function
End Class
