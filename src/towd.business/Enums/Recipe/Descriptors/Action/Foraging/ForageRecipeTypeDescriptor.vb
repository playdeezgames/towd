Imports towd.data

Public Class ForageRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New(recipeType As RecipeType, name As String, requiredLocationType As LocationType, outputItemType As ItemType)
        MyBase.New(recipeType, 1)
        SetDisplayName(name)
        SetRequiredLocationType(requiredLocationType)
        SetLocationStatisticMinimum(data.StatisticType.ForagingCounter, 1)
        SetLocationStatisticDelta(data.StatisticType.ForagingCounter, -1)
        SetCharacterStatisticDelta(data.StatisticType.ForagingCounter, 1)
        SetItemTypeOutputGenerator(outputItemType, New ForageRangeGenerator)
    End Sub
End Class
