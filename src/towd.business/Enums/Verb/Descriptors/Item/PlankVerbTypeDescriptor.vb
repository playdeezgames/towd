Imports towd.data

Friend Class PlankVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Plank, VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.Hatchet, 1)
        SetItemTypeOutputGenerator(ItemType.Hatchet, New FixedGenerator(1))
        SetItemTypeInputDurability(ItemType.Hatchet, 3)
        SetItemTypeInput(ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(ItemType.Hammer, New FixedGenerator(1))
        SetItemTypeInputDurability(ItemType.Hammer, 3)
        SetItemTypeInput(ItemType.Log, 1)
        SetItemTypeOutputGenerator(ItemType.Plank, New FixedGenerator(4))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
