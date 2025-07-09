Imports towd.data

Friend Class AddFuelCookingFirePlankVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.AddFuelCookingFirePlank, VerbCategoryType.AddFuel, 0)
        SetDisplayName("Add Plank to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(business.StatisticType.Fuel, 2)
        SetItemTypeInput(ItemType.Plank, 1)
    End Sub
End Class
