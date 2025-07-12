Imports towd.data

Friend Class SharpRockVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.SharpRock, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.Rock, 1)
        SetItemTypeInput(business.ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(business.ItemType.Hammer, New FixedGenerator(1))
        SetItemTypeOutputGenerator(business.ItemType.SharpRock, New FixedGenerator(1))
        SetItemTypeInputDurability(business.ItemType.Hammer, 1)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
