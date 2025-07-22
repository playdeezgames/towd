Imports towd.data

Friend Class FishVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Fish, business.VerbCategoryType.Fish, 1)
        SetDisplayName("Fish")
        SetRequiredLocationType(LocationType.Pond)
        SetItemTypeInputDurability(ItemType.FishingNet, 1)
        SetLocationStatisticMinimum(business.StatisticType.Fishing, 1)
        SetLocationStatisticDelta(business.StatisticType.Fishing, -1)
        SetCharacterStatisticDelta(business.StatisticType.Fishing, 1)
        SetItemTypeOutputGenerator(ItemType.RawFish, New FishRangeGenerator)
        SetFlavorText("Using a net to catch a fish. Could be worse, in a different game I made you have to use a spear.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
