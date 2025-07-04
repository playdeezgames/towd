Imports towd.data

Friend Class UnfiredBrickVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Public Sub New()
        MyBase.New(VerbType.UnfiredBrick, VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.PlantFiber, 1)
        SetItemTypeInput(ItemType.Clay, 1)
        SetItemTypeOutputGenerator(ItemType.UnfiredBrick, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
