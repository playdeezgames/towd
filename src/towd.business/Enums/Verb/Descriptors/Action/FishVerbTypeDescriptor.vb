Imports towd.data

Friend Class FishVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Fish, business.VerbCategoryType.Fish, 1)
        SetDisplayName("Fish")
        SetRequiredLocationType(LocationType.Pond)
        SetItemTypeInputDurability(ItemType.FishingNet, 1)
        SetLocationStatisticMinimum(business.StatisticType.Fishing, 1)
        SetLocationStatisticDelta(business.StatisticType.Fishing, -1)
        SetCharacterStatisticDelta(business.StatisticType.Fishing, 1)
        SetItemTypeOutputGenerator(ItemType.RawFish, New FishRangeGenerator)
    End Sub
End Class
