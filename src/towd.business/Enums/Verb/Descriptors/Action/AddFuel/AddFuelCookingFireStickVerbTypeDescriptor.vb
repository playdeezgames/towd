Friend Class AddFuelCookingFireStickVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.AddFuelCookingFireStick,
            business.VerbCategoryType.AddFuel,
            0)
        SetDisplayName("Add Stick to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(business.StatisticType.Fuel, 1)
        SetItemTypeInput(ItemType.Stick, 1)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
