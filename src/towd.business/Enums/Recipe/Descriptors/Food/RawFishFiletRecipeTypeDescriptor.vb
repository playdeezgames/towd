Imports towd.data

Friend Class RawFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.RawFishFilet, 1)
        SetItemTypeInput(ItemType.Knife, 1)
        SetItemTypeInput(ItemType.RawFish, 1)
        SetItemTypeInputDurability(ItemType.Knife, 1)
        SetItemTypeOutputGenerator(ItemType.Knife, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.FishGuts, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.FishHead, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.RawFishFilet, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
