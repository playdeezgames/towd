Imports towd.data

Public Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New(verbType As VerbType, name As String, requiredLocationType As LocationType, outputItemType As ItemType)
        MyBase.New(verbType, VerbCategoryType.Forage, 1)
        SetDisplayName(name)
        SetRequiredLocationType(requiredLocationType)
        SetLocationStatisticMinimum(data.StatisticType.ForagingCounter, 1)
        SetLocationStatisticDelta(data.StatisticType.ForagingCounter, -1)
        SetCharacterStatisticDelta(data.StatisticType.ForagingCounter, 1)
        SetItemTypeOutputGenerator(outputItemType, New ForageRangeGenerator)
    End Sub
End Class
