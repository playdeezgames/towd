Imports towd.data

Public Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New(
                  verbType As VerbType,
                  name As String,
                  requiredLocationType As LocationType,
                  outputItemType As String)
        MyBase.New(verbType, VerbCategoryType.Forage, 1)
        SetDisplayName(name)
        SetRequiredLocationType(requiredLocationType)
        SetLocationStatisticMinimum(business.StatisticType.ForagingCounter, 1)
        SetLocationStatisticDelta(business.StatisticType.ForagingCounter, -1)
        SetCharacterStatisticDelta(business.StatisticType.ForagingCounter, 1)
        SetItemTypeOutputGenerator(outputItemType, New ForageRangeGenerator)
    End Sub
End Class
