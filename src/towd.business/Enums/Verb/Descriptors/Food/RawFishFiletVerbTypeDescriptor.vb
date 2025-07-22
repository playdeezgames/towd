Imports towd.data

Friend Class RawFishFiletVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.RawFishFilet, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.Knife, 1)
        SetItemTypeInput(ItemType.RawFish, 1)
        SetItemTypeInputDurability(ItemType.Knife, 1)
        SetItemTypeOutputGenerator(ItemType.Knife, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.FishGuts, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.FishHead, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.RawFishFilet, New FixedCharacterWeightedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("What am I supposed to do with the fish head? Take it to a movie? I'll prolly have to pay to get it in.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
