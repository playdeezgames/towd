Imports towd.data

Friend Class AddFuelCookingFireLogVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.AddFuelCookingFireLog, 0)
        SetDisplayName("Add Log to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(data.StatisticType.Fuel, 4)
        SetItemTypeInput(ItemType.Log, 1)
    End Sub
End Class
