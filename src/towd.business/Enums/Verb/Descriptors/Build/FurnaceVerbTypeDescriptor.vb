Imports towd.data

Friend Class FurnaceVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Furnace, business.VerbCategoryType.Build, 1)
        SetItemTypeInput(business.ItemType.Brick, 8)
        SetRequiredLocationType(LocationType.Dirt)
        SetRequiredLocationType(LocationType.Grass)
        SetBuildsLocationType(LocationType.Furnace)
        SetCharacterStatisticDelta(StatisticType.BuildCounter, 1)
    End Sub
End Class
