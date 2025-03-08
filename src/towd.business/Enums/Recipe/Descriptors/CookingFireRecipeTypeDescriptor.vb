Imports towd.data

Friend Class CookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookingFire, 1)
        SetInput(ItemType.Rock, 8)
        SetInput(ItemType.Stick, 8)
        SetOutput(ItemType.CookingFire, 1)
    End Sub
    Protected Overrides Function Precondition(Character As ICharacter) As Boolean
        Dim locationType = Character.Location.EntityType.LocationType
        Return locationType = LocationType.Grass OrElse locationType = LocationType.Dirt
    End Function
    Protected Overrides Sub Predicate(character As ICharacter)
        character.RemoveItemOfType(data.ItemType.CookingFire.ToDescriptor)
        character.Location.EntityType = data.LocationType.CookingFire.ToDescriptor
    End Sub
End Class
