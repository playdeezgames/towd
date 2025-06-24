Friend Class CharcoalCookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CharcoalCookingFire, 1)
        SetItemTypeInput(data.ItemType.Log, 2)
        SetItemTypeOutputGenerator(data.ItemType.Charcoal, New FixedGenerator(1))
        SetRequiredLocationType(data.LocationType.CookingFire)
    End Sub
End Class
