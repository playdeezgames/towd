Imports towd.data

Friend Class CharcoalFurnaceVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.CharcoalFromFurnace, VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.Log, 1)
        SetItemTypeOutputGenerator(business.ItemType.Charcoal, New FixedGenerator(1))
        SetRequiredLocationType(business.LocationType.Furnace)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
