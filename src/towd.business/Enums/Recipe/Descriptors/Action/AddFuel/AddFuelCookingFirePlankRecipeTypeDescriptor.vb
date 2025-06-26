Imports towd.data

Friend Class AddFuelCookingFirePlankRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.AddFuelCookingFirePlank, 0)
        SetDisplayName("Add Plank to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(data.StatisticType.Fuel, 2)
        SetItemTypeInput(ItemType.Plank, 1)
    End Sub
End Class
