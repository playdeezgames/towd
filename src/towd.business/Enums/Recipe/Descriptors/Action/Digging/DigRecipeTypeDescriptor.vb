Imports towd.data

Public Class DigRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New(recipeType As VerbType, name As String, requiredLocationType As LocationType, outputItemType As ItemType)
        MyBase.New(recipeType, 1)
        SetDisplayName(name)
        SetRequiredLocationType(requiredLocationType)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetLocationStatisticMinimum(data.StatisticType.Digging, 1)
        SetLocationStatisticDelta(data.StatisticType.Digging, -1)
        SetCharacterStatisticDelta(data.StatisticType.Digging, 1)
        SetItemTypeOutputGenerator(outputItemType, New DigRangeGenerator)
    End Sub
End Class
