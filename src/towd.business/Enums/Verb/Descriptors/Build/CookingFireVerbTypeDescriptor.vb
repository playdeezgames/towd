﻿Imports towd.data

Friend Class CookingFireVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.CookingFire, business.VerbCategoryType.Build, 1)
        SetItemTypeInput(ItemType.Rock, 8)
        SetItemTypeInput(ItemType.Stick, 8)
        SetRequiredLocationType(LocationType.Dirt)
        SetRequiredLocationType(LocationType.Grass)
        SetBuildsLocationType(LocationType.CookingFire)
        SetCharacterStatisticDelta(StatisticType.BuildCounter, 1)
    End Sub
End Class
