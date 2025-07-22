Imports towd.data

Friend Class AddFuelCookingFirePlankVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.AddFuelCookingFirePlank,
            business.VerbCategoryType.AddFuel,
            0)
        SetDisplayName("Add Plank to Cooking Fire")
        SetRequiredLocationType(LocationType.CookingFire)
        SetLocationStatisticDelta(business.StatisticType.Fuel, 2)
        SetItemTypeInput(ItemType.Plank, 1)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
