Imports towd.data

Friend Class FishVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Fish, 1)
        SetDisplayName("Fish")
        SetRequiredLocationType(LocationType.Pond)
        SetItemTypeInputDurability(ItemType.FishingNet, 1)
        SetLocationStatisticMinimum(data.StatisticType.Fishing, 1)
        SetLocationStatisticDelta(data.StatisticType.Fishing, -1)
        SetCharacterStatisticDelta(data.StatisticType.Fishing, 1)
        SetItemTypeOutputGenerator(ItemType.RawFish, New FishRangeGenerator)
    End Sub
End Class
