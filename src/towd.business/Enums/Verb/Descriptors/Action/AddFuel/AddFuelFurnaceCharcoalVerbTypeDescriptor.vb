Imports towd.data

Friend Class AddFuelFurnaceCharcoalVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.AddFuelFurnaceCharcoal, business.VerbCategoryType.AddFuel, 0)
        SetDisplayName("Add Charcoal to Furnace")
        SetRequiredLocationType(LocationType.Furnace)
        SetLocationStatisticDelta(business.StatisticType.Fuel, 4)
        SetItemTypeInput(ItemType.Charcoal, 1)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
