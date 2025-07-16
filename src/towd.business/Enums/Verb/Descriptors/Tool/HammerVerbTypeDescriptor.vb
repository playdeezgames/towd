Imports towd.data

Friend Class HammerVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Hammer, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.Rock, 1)
        SetItemTypeInput(ItemType.Twine, 1)
        SetItemTypeInput(ItemType.Stick, 1)
        SetItemTypeOutputGenerator(ItemType.Hammer, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("Please hammer, don't hurt em.")
    End Sub
End Class
