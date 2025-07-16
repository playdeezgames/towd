Imports towd.data

Friend Class CharcoalCookingFireVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.CharcoalFromCookingFire, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.Log, 2)
        SetItemTypeOutputGenerator(business.ItemType.Charcoal, New FixedGenerator(1))
        SetRequiredLocationType(business.LocationType.CookingFire)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("The act of burning wood until it is something better at burn than wood. Its like woodception.")
    End Sub
End Class
