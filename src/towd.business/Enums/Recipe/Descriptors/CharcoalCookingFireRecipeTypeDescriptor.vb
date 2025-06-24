Friend Class CharcoalCookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CharcoalCookingFire, 1)
        SetItemTypeInput(data.ItemType.Log, 2)
        SetItemTypeOutput(data.ItemType.Charcoal, 1)
        SetRequiredLocationType(data.LocationType.CookingFire)
    End Sub
End Class
