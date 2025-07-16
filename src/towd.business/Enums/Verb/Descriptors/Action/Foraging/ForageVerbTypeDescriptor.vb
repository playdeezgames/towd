Imports towd.data

Public Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New(
                  verbType As String,
                  name As String,
                  requiredLocationType As String,
                  outputItemType As String,
                  flavorText As String)
        MyBase.New(verbType, business.VerbCategoryType.Forage, 1)
        SetDisplayName(name)
        SetRequiredLocationType(requiredLocationType)
        SetLocationStatisticMinimum(business.StatisticType.ForagingCounter, 1)
        SetLocationStatisticDelta(business.StatisticType.ForagingCounter, -1)
        SetCharacterStatisticDelta(business.StatisticType.ForagingCounter, 1)
        SetItemTypeOutputGenerator(outputItemType, New ForageRangeGenerator)
        SetFlavorText(flavorText)
    End Sub
End Class
