Imports towd.data

Friend Class ChopVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Chop, VerbCategoryType.Chop, 1)
        SetDisplayName("Chop")
        SetRequiredLocationType(LocationType.Pine)
        SetItemTypeInputDurability(ItemType.Hatchet, 1)
        SetLocationStatisticMinimum(business.StatisticType.Chopping, 1)
        SetLocationStatisticDelta(business.StatisticType.Chopping, -1)
        SetCharacterStatisticDelta(business.StatisticType.Chopping, 1)
        SetItemTypeOutputGenerator(ItemType.Log, New ChopRangeGenerator)
    End Sub
End Class
