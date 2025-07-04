Imports towd.data

Friend Class ChopVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Chop, VerbCategoryType.Chop, 1)
        SetDisplayName("Chop")
        SetRequiredLocationType(LocationType.Pine)
        SetItemTypeInputDurability(ItemType.Hatchet, 1)
        SetLocationStatisticMinimum(data.StatisticType.Chopping, 1)
        SetLocationStatisticDelta(data.StatisticType.Chopping, -1)
        SetCharacterStatisticDelta(data.StatisticType.Chopping, 1)
        SetItemTypeOutputGenerator(ItemType.Log, New ChopRangeGenerator)
    End Sub
End Class
