Imports towd.data

Friend Class AddFuelCookingFireCharcoalRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.AddFuelCookingFireCharcoal, 0)
        SetDisplayName("Add Charcoal to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(data.StatisticType.Fuel, 8)
        SetItemTypeInput(ItemType.Charcoal, 1)
    End Sub
End Class
