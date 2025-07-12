Imports towd.data

Friend Class AddFuelCookingFireCharcoalVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.AddFuelCookingFireCharcoal, business.VerbCategoryType.AddFuel, 0)
        SetDisplayName("Add Charcoal to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(business.StatisticType.Fuel, 8)
        SetItemTypeInput(ItemType.Charcoal, 1)
    End Sub
End Class
