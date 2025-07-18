﻿Imports towd.data

Friend Class CharcoalFurnaceVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.CharcoalFromFurnace, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.Log, 1)
        SetItemTypeOutputGenerator(business.ItemType.Charcoal, New FixedGenerator(1))
        SetRequiredLocationType(business.LocationType.Furnace)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("Using charcoal to make charcoal. That's like charcoalception.")
    End Sub
End Class
