Friend Class CharcoalCookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CharcoalCookingFire, 1)
        SetInput(data.ItemType.Log, 2)
        SetOutput(data.ItemType.Charcoal, 1)
    End Sub
    Protected Overrides Function Precondition(Character As ICharacter) As Boolean
        Dim locationType = Character.Location.EntityType.LocationType
        Return locationType = data.LocationType.CookingFire
    End Function
End Class
