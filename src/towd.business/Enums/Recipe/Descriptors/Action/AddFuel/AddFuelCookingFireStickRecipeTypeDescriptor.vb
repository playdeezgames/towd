Imports towd.data

Friend Class AddFuelCookingFireStickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.AddFuelCookingFireStick, 0)
        SetDisplayName("Add Stick to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(data.StatisticType.Fuel, 1)
        SetItemTypeInput(ItemType.Stick, 1)
    End Sub
End Class
