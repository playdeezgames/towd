Imports towd.data

Friend Class AddFuelCookingFireLogRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.AddFuelCookingFireLog, 0)
        SetDisplayName("Add Log to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(data.StatisticType.Fuel, 4)
        SetItemTypeInput(ItemType.Log, 1)
    End Sub
End Class
