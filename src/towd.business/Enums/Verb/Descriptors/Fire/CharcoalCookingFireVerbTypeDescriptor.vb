Imports towd.data

Friend Class CharcoalCookingFireVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.CharcoalFromCookingFire, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.Log, 2)
        SetItemTypeOutputGenerator(business.ItemType.Charcoal, New FixedGenerator(1))
        SetRequiredLocationType(business.LocationType.CookingFire)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
