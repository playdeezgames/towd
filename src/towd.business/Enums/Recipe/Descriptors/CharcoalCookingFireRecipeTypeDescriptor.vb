Friend Class CharcoalCookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CharcoalCookingFire, 1)
        SetInput(data.ItemType.Log, 2)
        SetOutput(data.ItemType.Charcoal, 1)
        SetRequiredLocation(data.LocationType.CookingFire)
    End Sub
End Class
