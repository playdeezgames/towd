Imports towd.data

Friend Class CookedGrubVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.CookedGrub, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.Grub, 1)
        SetItemTypeInput(ItemType.SharpStick, 1)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetItemTypeOutputGenerator(ItemType.SharpStick, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.CookedGrub, New FixedCharacterWeightedGenerator(1))
        SetRequiredLocationType(LocationType.CookingFire)
        SetCharacterStatisticDelta(StatisticType.CookingCounter, 1)
        SetFlavorText("This doesn't make the grub taste any better. It just makes it hot.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
