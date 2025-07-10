Imports towd.data

Public Class DigVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New(
                  verbType As VerbType,
                  name As String,
                  requiredLocationType As LocationType,
                  outputItemType As String)
        MyBase.New(verbType, VerbCategoryType.Dig, 1)
        SetDisplayName(name)
        SetRequiredLocationType(requiredLocationType)
        SetItemTypeInputDurability(ItemType.SharpStick, 1)
        SetLocationStatisticMinimum(business.StatisticType.Digging, 1)
        SetLocationStatisticDelta(business.StatisticType.Digging, -1)
        SetCharacterStatisticDelta(business.StatisticType.Digging, 1)
        SetItemTypeOutputGenerator(outputItemType, New DigRangeGenerator)
    End Sub
End Class
