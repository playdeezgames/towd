Imports towd.data

Friend Class BrickVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Brick, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.UnfiredBrick, 1)
        SetItemTypeOutputGenerator(ItemType.Brick, New FixedGenerator(1))
        SetRequiredLocationType(LocationType.CookingFire)
        SetRequiredLocationType(LocationType.Furnace)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
