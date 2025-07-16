Imports towd.data

Friend Class RawFishFiletVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.RawFishFilet, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.Knife, 1)
        SetItemTypeInput(ItemType.RawFish, 1)
        SetItemTypeInputDurability(ItemType.Knife, 1)
        SetItemTypeOutputGenerator(ItemType.Knife, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.FishGuts, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.FishHead, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.RawFishFilet, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("What am I supposed to do with the fish head? Take it to a movie? I'll prolly have to pay to get it in.")
    End Sub
End Class
