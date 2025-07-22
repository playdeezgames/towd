Friend Class CookedFishFiletVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.CookedFishFilet, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.RawFishFilet, 1)
        SetItemTypeInput(ItemType.SharpStick, 1)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetItemTypeOutputGenerator(ItemType.SharpStick, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.CookedFishFilet, New FixedCharacterWeightedGenerator(1))
        SetRequiredLocationType(LocationType.CookingFire)
        SetCharacterStatisticDelta(StatisticType.CookingCounter, 1)
        SetFlavorText("Now, if only there were some salt and lemon zest...")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
