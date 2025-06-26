Imports towd.data

Friend Class AddFuelFurnaceCharcoalRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.AddFuelFurnaceCharcoal, 0)
        SetDisplayName("Add Charcoal to Furnace")
        SetRequiredLocationType(LocationType.Furnace)
        SetLocationStatisticDelta(data.StatisticType.Fuel, 4)
        SetItemTypeInput(ItemType.Charcoal, 1)
    End Sub
End Class
