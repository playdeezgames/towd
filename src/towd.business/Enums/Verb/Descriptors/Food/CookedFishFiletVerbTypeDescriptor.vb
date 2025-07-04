Imports towd.data

Friend Class CookedFishFiletVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.CookedFishFilet, VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.RawFishFilet, 1)
        SetItemTypeInput(ItemType.SharpStick, 1)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetItemTypeOutputGenerator(ItemType.SharpStick, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.CookedFishFilet, New FixedGenerator(1))
        SetRequiredLocationType(LocationType.CookingFire)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
