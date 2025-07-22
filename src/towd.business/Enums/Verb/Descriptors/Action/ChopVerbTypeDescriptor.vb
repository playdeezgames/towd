Imports towd.data

Friend Class ChopVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Chop, business.VerbCategoryType.Chop, 1)
        SetDisplayName("Chop")
        SetRequiredLocationType(LocationType.Pine)
        SetItemTypeInputDurability(ItemType.Hatchet, 1)
        SetLocationStatisticMinimum(business.StatisticType.Chopping, 1)
        SetLocationStatisticDelta(business.StatisticType.Chopping, -1)
        SetCharacterStatisticDelta(business.StatisticType.Chopping, 1)
        SetItemTypeOutputGenerator(ItemType.Log, New ChopRangeGenerator)
        SetFlavorText("As this is not a cherry tree, you can go ahead and tell lies.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
