Imports towd.data

Friend Class CookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookingFire, 1)
        SetInput(ItemType.Rock, 8)
        SetInput(ItemType.Stick, 8)
        SetOutput(ItemType.CookingFire, 1)
        SetRequiredLocation(LocationType.Dirt)
        SetRequiredLocation(LocationType.Grass)
    End Sub
    Protected Overrides Sub Predicate(character As ICharacter)
        character.RemoveItemOfType(data.ItemType.CookingFire.ToDescriptor)
        character.Location.EntityType = data.LocationType.CookingFire.ToDescriptor
    End Sub
End Class
